using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;
using System;

namespace Reqnroll.Actions.LambdaTest.DriverInitialisers;

internal class LambdaTestInternetExplorerDriverInitialiser : InternetExplorerDriverInitialiser
{
    private readonly LambdaTestDriverInitialiser _lambdaTestDriverInitialiser;

    public LambdaTestInternetExplorerDriverInitialiser(LambdaTestDriverInitialiser lambdaTestDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(
        seleniumConfiguration, credentialProvider)
    {
        _lambdaTestDriverInitialiser = lambdaTestDriverInitialiser;
    }

    protected override void AddDefaultCapabilities(InternetExplorerOptions options)
    {
        base.AddDefaultCapabilities(options);
        _lambdaTestDriverInitialiser.AddDefaultCapabilities(options);
    }

    protected override IWebDriver CreateWebDriver(InternetExplorerOptions options)
    {
        return _lambdaTestDriverInitialiser.GetWebDriver(options);
    }
}