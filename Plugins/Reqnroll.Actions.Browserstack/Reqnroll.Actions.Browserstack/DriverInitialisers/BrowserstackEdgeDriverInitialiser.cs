using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;

namespace Reqnroll.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackEdgeDriverInitialiser : EdgeDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackEdgeDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(EdgeOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }
}