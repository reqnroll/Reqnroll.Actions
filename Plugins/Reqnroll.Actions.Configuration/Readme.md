# Reqnroll.Actions.Configuration

[![Nuget](https://img.shields.io/nuget/v/Reqnroll.Actions.Configuration)](https://www.nuget.org/packages/Reqnroll.Actions.Configuration/)

This Reqnroll.Action is used to read configuration values out of the `reqnroll.actions.json` file. You can also provide configurations for multiple targets, to run your scenarios with different configurations.

This Reqnroll.Action is used by every Reqnroll.Action that has a configuration.

## Included Features

- Reading configuration values from `reqnroll.actions.json`
- Supports targets via `reqnroll.actions.%TARGET_NAME%.json` files.
- helper classes to read out the configuration values.

## Configuration

You can configure this plugin via the  `reqnroll.actions.json`.

Example:

``` json
{
  "your_configuration": "value of the property"
}
```

### Multiple target configurations

This plugin supports tagreting of multiple configurations at runtime. For each configuration you provide, a class will be generated in your feature's code behind file when you build the project. This means that for any given test, the test will be executed against each target.

Example:

```reqnroll.actions.json```

``` json
{
  "your_configuration": "value of the property",
  "another_configuration": "another value"
}
```


```reqnroll.actions.TARGET_1.json```

``` json
{
  "your_configuration": "TARGET 1 overrides this value"
}
```

```reqnroll.actions.TARGET_2.json```

``` json
{
  "your_configuration": "TARGET 2 overrides this value"
}
```

``` csharp
[Binding]
public class A_Binding_Class
{
    private readonly IReqnrollActionsConfiguration _reqnrollActionsConfiguration;

    public A_Binding_Class(IReqnrollActionsConfiguration reqnrollActionsConfiguration)
    {
        _reqnrollActionsConfiguration = reqnrollActionsConfiguration;
    }

    public string GetConfigurationValue()
    {
        return _reqnrollActionsConfiguration.Get("your_configuration", string.Empty);
    }
}
```

With this configuration, you are getting every scenario twice in your test explorer.

The result of the `GetConfigurationValue` is the following:


| Target Name | your_configuration            | another_configuration |
| ----------- | ----------------------------- | --------------------- |
| Target_1    | TARGET 1 overrides this value | another value         |
| Target_2    | TARGET 2 overrides this value | another value         |


## APIs

### IReqnrollActionsConfiguration

#### string Get(string path);
Get your configuration value as string. Throws an error if the configuration value doesn't exist.
#### string Get(string path, string defaultValue);
Get your configuration value as string. If configuration value can't be found, the `defaultValue` is returned
#### double? GetDouble(string path);
Get your configuration value as `double`. 
#### string[] GetArray(string path);
Get your configuration value as a `string[]`. This is used to get a list from your configuration json file.
#### Dictionary<string, string> GetDictionary(string path);
Get the child elements of a node in your configuration json file.

## How to get it

Add the latest version of the `Reqnroll.Actions.Configuration` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/Reqnroll.Actions.Configuration)](https://www.nuget.org/packages/Reqnroll.Actions.Configuration/)
