# A Hessian binary protocol #

[![Build Status](https://dev.azure.com/tolmachewladimir/tolmachewladimir/_apis/build/status/VlaTo.Hessian.NET?branchName=master)](https://dev.azure.com/tolmachewladimir/tolmachewladimir/_build/latest?definitionId=2?branchName=master)
[![Nuget Downloads](https://img.shields.io/nuget/vpre/LibraProgramming.Hessian.svg?logo=nuget)](https://www.nuget.org/packages/LibraProgramming.Hessian/)

This is implementation of Hessian 2.0 Web Service Protocol serializer for the .NET in the C#.

## Description ##
This implementation is binary protocol serializer. It can serialize/deserialize simple value-types and complex object graphs. For example, we has class:
```C#
[DataContract]
public sealed class DummyClass {
  [DataMember(Name = "field")]
  public string Field1
  {
    get;
    set;
  }
}
```
### Serializing the object graph ###
```C#
var graph = new DummyClass
{
  Field1 = "Lorem Ipsum"
};

var settings = new HessianSerializerSettings();
var serializer = new DataContractHessianSerializer(typeof(DummyClass), settings);

using (var stream = new MemoryStream())
{
  serializer.WriteObject(stream, graph);
}
```
### Deserializing the object graph ###
```C#
var settings = new HessianSerializerSettings();
var serializer = new DataContractHessianSerializer(typeof(DummyClass), settings);

// your data retrieval goes here
var packet = new byte[...];

using (var stream = new MemoryStream(packet))
{
  var graph = (DummyClass) serializer.ReadObject(stream);
}
```

### Links ###
* [Official Hessian 2.0 documentation](http://hessian.caucho.com/doc/hessian-serialization.html)
