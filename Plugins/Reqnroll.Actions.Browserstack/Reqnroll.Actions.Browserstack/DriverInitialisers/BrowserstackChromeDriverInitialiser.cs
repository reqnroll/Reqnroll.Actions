using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;

namespace Reqnroll.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackChromeDriverInitialiser : ChromeDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackChromeDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(ChromeOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }

}