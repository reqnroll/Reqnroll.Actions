using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;

namespace Reqnroll.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackFirefoxDriverInitialiser : FirefoxDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackFirefoxDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(FirefoxOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }
    
}