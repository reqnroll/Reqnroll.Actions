using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;
using System;

namespace Reqnroll.Actions.LambdaTest.DriverInitialisers;

internal class LambdaTestEdgeDriverInitialiser : EdgeDriverInitialiser
{
    private readonly LambdaTestDriverInitialiser _lambdaTestDriverInitialiser;

    public LambdaTestEdgeDriverInitialiser(LambdaTestDriverInitialiser lambdaTestDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(
        seleniumConfiguration, credentialProvider)
    {
        _lambdaTestDriverInitialiser = lambdaTestDriverInitialiser;
    }

    protected override void AddDefaultCapabilities(EdgeOptions options)
    {
        base.AddDefaultCapabilities(options);
        _lambdaTestDriverInitialiser.AddDefaultCapabilities(options);
    }

    protected override IWebDriver CreateWebDriver(EdgeOptions options)
    {
        return _lambdaTestDriverInitialiser.GetWebDriver(options);
    }
}