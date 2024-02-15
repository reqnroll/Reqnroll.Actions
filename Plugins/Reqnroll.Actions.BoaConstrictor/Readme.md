# Reqnroll.Actions.BoaConstrictor

This Reqnroll.Action will help you use [Boa-Constrictor](https://github.com/q2ebanking/boa-constrictor) together with Reqnroll to use the Screenplay Pattern for Selenium even more easily.  
In the background it is using [Reqnroll.Actions.Selenium](../Reqnroll.Actions.Selenium), so all functionality of it is also available, if you are using Boa-Constrictor.

Boa-Constrictor is configured out of the box. The logger is connected to the [Reqnroll Output API](https://docs.reqnroll.net/projects/reqnroll/en/latest/outputapi/outputapi.html).

## Configuration

No configuration options available

## How to use it

You can request the Actor by Context Injection.

Usage:

``` csharp
[Binding]
public class StepImplementation
{
    private Actor _actor;

    public StepImplementation(Actor actor)
    {
        _actor = actor;
    }

    [Given("")]
    public void SomeStep()
    {
        var currentUrl = _actor.AskingFor(CurrentUrl.FromBrowser());
    }
}
```

## How to get it

Add the latest version of the `Reqnroll.Actions.BoaConstrictor` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/Reqnroll.Actions.BoaConstrictor)](https://www.nuget.org/packages/Reqnroll.Actions.BoaConstrictor/)
