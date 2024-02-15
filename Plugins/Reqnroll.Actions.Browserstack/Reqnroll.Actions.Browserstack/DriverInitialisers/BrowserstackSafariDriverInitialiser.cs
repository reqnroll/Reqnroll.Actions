using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;

namespace Reqnroll.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackSafariDriverInitialiser : SafariDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackSafariDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(SafariOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }
}