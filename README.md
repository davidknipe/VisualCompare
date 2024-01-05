# Visual Compare for Optimizely CMS 11 and CMS 12
Add a red/green visual comparison option for the Optimizely CMS

![Visual Compare mode for Optimizely CMS](/docs/visualcompare.png?raw=true)

See more information here: https://www.david-tec.com/2018/03/visual-compare-option-available-for-episerver-11/

----

## Installation

Install the package directly from the Optimizely Nuget repository.

``` 
dotnet add package UNRVLD.ODP.VisitorGroups
```
```
Install-Package UNRVLD.ODP.VisitorGroups
```

## Configuration (.NET 5.0/6.0/7.0/8.0)

*Startup.cs*
``` c#
// Adds the registration for visitor groups
services.AddVisualCompareMode();
```

## Configuration (.Net Framework)

None required
