using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;

namespace Reqnroll.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackInternetExplorerDriverInitialiser : InternetExplorerDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackInternetExplorerDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(InternetExplorerOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }
}