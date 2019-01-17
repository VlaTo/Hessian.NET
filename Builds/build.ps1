properties {
  $zipFileName = "Json120r1.zip"
  $majorVersion = "12.0"
  $majorWithReleaseVersion = "12.0.1"
  $nugetPrerelease = "beta1"
  $version = GetVersion $majorWithReleaseVersion
  $packageId = "Newtonsoft.Json"
  $signAssemblies = $false
  $signKeyPath = "C:\Development\Releases\newtonsoft.snk"
  $buildDocumentation = $false
  $buildNuGet = $true
  $msbuildVerbosity = 'minimal'
  $treatWarningsAsErrors = $false
  $workingName = if ($workingName) {$workingName} else {"Working"}
  $netCliChannel = "2.0"
  $netCliVersion = "2.1.500"
  $nugetUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

  $baseDir  = resolve-path ..
  $buildDir = "$baseDir\Build"
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