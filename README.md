# testing-research
Simple Implementations of common software testing tools

**ContainerLibrary** is an IoC container

**SampleUnitTests** help test the TestRunner

**TestRunner** runs the Unit Tests

**UnitTestLibrary** defines the components for unit testing

**RoslynControlFlowSample** uses the Roslyn API to perform some basic Control Flow Analysis

**RoslynDataFlowSample** uses the Roslyn API to perform some basic Data Flow Analysis

## TODO:
1. Expanded the TestOracles in the Assert class
2. Write a code coverage tool that instruments the test to provide basic code coverage metrics

## Building
This project uses Microsoft .NET Core 2.0 [More information about .NET Core is available here](https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-net-core-2-0/). From terminal, in the directory that the sln is in type: 

```
dotnet build testing-research.sln
```

## TestRunner
To execute the TestRunner from the terminal, in the directory that the sln is in type: 

```
dotnet .\TestRunner\bin\Debug\netcoreapp2.0\TestRunner.dll .\SampleUnitTests\bin\Debug\netcoreapp2.0\SampleUnitTests.dll
```
