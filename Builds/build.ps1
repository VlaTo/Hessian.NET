properties {
  $zipFileName = "Json120r1.zip"
  $majorVersion = "0.1"
  $majorWithReleaseVersion = "0.1.0"
  $nugetPrerelease = "dev"
  $version = GetVersion $majorWithReleaseVersion
  $packageId = "LibraProgramming.Hessian"
  $signAssemblies = $false
  $signKeyPath = "C:\Development\Releases\libraprogramming.snk"
  $buildDocumentation = $false
  $buildNuGet = $true
  $msbuildVerbosity = 'minimal'
  $treatWarningsAsErrors = $false
  $workingName = if ($workingName) {$workingName} else {"Working"}
  $netCliChannel = "2.0"
  $netCliVersion = "2.1.500"
  $nugetUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

  $baseDir  = resolve-path ..
  $buildDir = "$baseDir\Builds"
  $sourceDir = "$baseDir\Source"
  $releaseDir = "$baseDir\Release"
  $workingDir = "$baseDir\$workingName"

  $nugetPath = "$buildDir\Temp\nuget.exe"
  $vswhereVersion = "2.3.2"
  $vswherePath = "$buildDir\Temp\vswhere.$vswhereVersion"
  $nunitConsoleVersion = "3.8.0"
  $nunitConsolePath = "$buildDir\Temp\NUnit.ConsoleRunner.$nunitConsoleVersion"

  $builds = @(
    @{Framework = "netstandard2.0"; TestFramework = "netcoreapp2.1"; Enabled=$true},
    @{Framework = "netstandard1.3"; TestFramework = "netcoreapp1.1"; Enabled=$true},
    @{Framework = "net45"; TestFramework = "net46"; Enabled=$true},
    @{Framework = "net40"; Enabled=$true},
  )
}

framework '4.6x86'

task default -depends Build

# Ensure a clean working directory
task Clean {
  Write-Host "Setting location to $baseDir"
  Set-Location $baseDir

  if (Test-Path -path $workingDir)
  {
    Write-Host "Deleting existing working directory $workingDir"

    Execute-Command -command { del $workingDir -Recurse -Force }
  }

  Write-Host "Creating working directory $workingDir"
  New-Item -Path $workingDir -ItemType Directory

}

# Build each solution, optionally signed
task Build -depends Clean {
  $script:enabledBuilds = $builds | ? {$_.Enabled}
  Write-Host -ForegroundColor Green "Found $($script:enabledBuilds.Length) enabled builds"

  mkdir "$buildDir\Temp" -Force
  
  EnsureDotNetCli
  EnsureNuGetExists
  EnsureNuGetPackage "vswhere" $vswherePath $vswhereVersion
  EnsureNuGetPackage "NUnit.ConsoleRunner" $nunitConsolePath $nunitConsoleVersion

  $script:msBuildPath = GetMsBuildPath
  Write-Host "MSBuild path $script:msBuildPath"

  NetCliBuild
}

function GetMsBuildPath()
{
  $path = & $vswherePath\tools\vswhere.exe -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
  if (!($path)) {
    throw "Could not find Visual Studio install path"
  }
  return join-path $path 'MSBuild\15.0\Bin\MSBuild.exe'
}

function NetCliBuild()
{
  $projectPath = "$sourceDir\Newtonsoft.Json.sln"
  $libraryFrameworks = ($script:enabledBuilds | Select-Object @{Name="Framework";Expression={$_.Framework}} | select -expand Framework) -join ";"
  $testFrameworks = ($script:enabledBuilds | Select-Object @{Name="Resolved";Expression={if ($_.TestFramework -ne $null) { $_.TestFramework } else { $_.Framework }}} | select -expand Resolved) -join ";"

  $additionalConstants = switch($signAssemblies) { $true { "SIGNED" } default { "" } }

  Write-Host -ForegroundColor Green "Restoring packages for $libraryFrameworks in $projectPath"
  Write-Host

  exec { & $script:msBuildPath "/t:restore" "/v:$msbuildVerbosity" "/p:Configuration=Release" "/p:LibraryFrameworks=`"$libraryFrameworks`"" "/p:TestFrameworks=`"$testFrameworks`"" "/m" $projectPath | Out-Default } "Error restoring $projectPath"

  Write-Host -ForegroundColor Green "Building $libraryFrameworks in $projectPath"
  Write-Host

  $assemblyVersion = $majorVersion + '.0.0'

  exec { & $script:msBuildPath "/t:build" "/v:$msbuildVerbosity" $projectPath "/p:Configuration=Release" "/p:LibraryFrameworks=`"$libraryFrameworks`"" "/p:TestFrameworks=`"$testFrameworks`"" "/p:AssemblyOriginatorKeyFile=$signKeyPath" "/p:SignAssembly=$signAssemblies" "/p:TreatWarningsAsErrors=$treatWarningsAsErrors" "/p:AdditionalConstants=$additionalConstants" "/p:GeneratePackageOnBuild=$buildNuGet" "/p:ContinuousIntegrationBuild=true" "/p:PackageId=$packageId" "/p:VersionPrefix=$majorWithReleaseVersion" "/p:VersionSuffix=$nugetPrerelease" "/p:AssemblyVersion=$assemblyVersion" "/p:FileVersion=$version" "/m" }
}

function EnsureDotNetCli()
{
  Write-Host "Downloading dotnet-install.ps1"

  # https://stackoverflow.com/questions/36265534/invoke-webrequest-ssl-fails
  [Net.ServicePointManager]::SecurityProtocol = 'TLS12'
  Invoke-WebRequest `
    -Uri "https://dot.net/v1/dotnet-install.ps1" `
    -OutFile "$buildDir\Temp\dotnet-install.ps1"

  exec { & $buildDir\Temp\dotnet-install.ps1 -Channel $netCliChannel -Version $netCliVersion | Out-Default }
}

function EnsureNuGetExists()
{
  if (!(Test-Path $nugetPath))
  {
    Write-Host "Couldn't find nuget.exe. Downloading from $nugetUrl to $nugetPath"
    (New-Object System.Net.WebClient).DownloadFile($nugetUrl, $nugetPath)
  }
}

function EnsureNuGetPackage($packageName, $packagePath, $packageVersion)
{
  if (!(Test-Path $packagePath))
  {
    Write-Host "Couldn't find $packagePath. Downloading with NuGet"
    exec { & $nugetPath install $packageName -OutputDirectory $buildDir\Temp -Version $packageVersion -ConfigFile "$sourceDir\nuget.config" | Out-Default } "Error restoring $packagePath"
  }
}