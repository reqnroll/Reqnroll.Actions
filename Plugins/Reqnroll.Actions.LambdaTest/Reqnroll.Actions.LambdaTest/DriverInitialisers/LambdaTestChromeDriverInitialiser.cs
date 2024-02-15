using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;
using System;

namespace Reqnroll.Actions.LambdaTest.DriverInitialisers;

internal class LambdaTestChromeDriverInitialiser : ChromeDriverInitialiser
{
    private readonly LambdaTestDriverInitialiser _lambdaTestDriverInitialiser;

    public LambdaTestChromeDriverInitialiser(LambdaTestDriverInitialiser lambdaTestDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(
        seleniumConfiguration, credentialProvider)
    {
        _lambdaTestDriverInitialiser = lambdaTestDriverInitialiser;
    }


    protected override IWebDriver CreateWebDriver(ChromeOptions options)
    {
        return _lambdaTestDriverInitialiser.GetWebDriver(options);
    }

    protected override void AddDefaultCapabilities(ChromeOptions options)
    {
        base.AddDefaultCapabilities(options);
        _lambdaTestDriverInitialiser.AddDefaultCapabilities(options);
    }
}