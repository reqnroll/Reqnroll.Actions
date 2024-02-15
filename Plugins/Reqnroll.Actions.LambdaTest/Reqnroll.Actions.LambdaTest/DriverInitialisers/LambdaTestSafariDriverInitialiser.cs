using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;
using System;

namespace Reqnroll.Actions.LambdaTest.DriverInitialisers;

internal class LambdaTestSafariDriverInitialiser : SafariDriverInitialiser
{
    private readonly LambdaTestDriverInitialiser _lambdaTestDriverInitialiser;

    public LambdaTestSafariDriverInitialiser(LambdaTestDriverInitialiser lambdaTestDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(
        seleniumConfiguration, credentialProvider)
    {
        _lambdaTestDriverInitialiser = lambdaTestDriverInitialiser;
    }

    protected override void AddDefaultCapabilities(SafariOptions options)
    {
        base.AddDefaultCapabilities(options);
        _lambdaTestDriverInitialiser.AddDefaultCapabilities(options);
    }

    protected override IWebDriver CreateWebDriver(SafariOptions options)
    {
        return _lambdaTestDriverInitialiser.GetWebDriver(options);
    }
}