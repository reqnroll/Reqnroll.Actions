using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.Hoster;
using System;

namespace Reqnroll.Actions.Selenium.DriverInitialisers;

public class EdgeDriverInitialiser : DriverInitialiser<EdgeOptions>
{
    private static readonly Lazy<string?> EdgeWebDriverFilePath =
        new Lazy<string?>(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));

    public EdgeDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) :
        base(seleniumConfiguration, credentialProvider)
    {
    }


    protected override void AddDefaultCapabilities(EdgeOptions options)
    {
        
    }

    protected override IWebDriver CreateWebDriver(EdgeOptions options)
    {
        return string.IsNullOrWhiteSpace(EdgeWebDriverFilePath.Value)
            ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
            : new EdgeDriver(EdgeDriverService.CreateDefaultService(EdgeWebDriverFilePath.Value), options,
                TimeSpan.FromSeconds(120));
    }
}