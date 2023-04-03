<!-- PROJECT LOGO -->
  <br />
    <p align="center">
    <img src="img/logo.png" alt="Logo" width="130" height="130">
  </a>
  <h1 align="center">Validate Or Throw</h1>
  <p align="center">
    Implement the Options Pattern with a single line of code, including validation on start and via Data Annotations.
  </p>

# How To

The option class you want to be added and validated must inherit from the following interface:

``` C#
public interface IValidatedOption
{
    /// <summary>
    /// Name of the section in the appsettings.json (or secrets) file.
    /// </summary>
    public string SectionName { get; }
}
```

Like so:

``` C#
public class ExampleOption : IValidatedOption
{
    public string SectionName => "Example";
}
```

Then you will be able to add you option as a singleton like so:

``` C#
builder.Services.AddOptionsOrThrow<ExampleOption>();
```

Note that the extension will throw:
- a new `InvalidOperationException` when a section does not exist.
-  a new `OptionsValidationException` when a requirement defined via [Data Annotations](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0) is not met.