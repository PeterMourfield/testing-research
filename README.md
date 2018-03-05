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

## TestRunner
To execute the TestRunner from the command line:
1. navigate to testing-research\TestRunner\bin\Debug\netcoreapp2.0
2. type dotnet TestRunner.dll ../../../../SampleUnitTests/bin/Debug/netcoreapp2.0/SampleUnitTests.dll
