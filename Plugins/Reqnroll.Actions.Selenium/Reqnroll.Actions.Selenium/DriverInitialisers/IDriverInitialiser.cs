using OpenQA.Selenium;

namespace Reqnroll.Actions.Selenium.DriverInitialisers
{
    public interface IDriverInitialiser
    {
        IWebDriver Initialise();
    }
}