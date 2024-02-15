using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using Reqnroll.Actions.Appium.Configuration.Android;
using Reqnroll.Actions.Appium.Configuration.WindowsAppDriver;
using Reqnroll.Actions.Appium.Server;
using System;

namespace Reqnroll.Actions.Appium.Driver
{
    public interface IDriverFactory
    {
        AndroidDriver<AndroidElement> GetAndroidDriver(IAppiumServer appiumServer, IDriverOptions driverOptions, IAndroidConfiguration appiumConfiguration);

        void GetXCUITestDriver();

        void GetEspressoDriver();

        WindowsDriver<WindowsElement> GetWindowsAppDriver(IWindowsAppDriverConfiguration windowsAppDriverConfiguration, IDriverOptions driverOptions, Uri driverUrl);

        void GetMacDriver();
    }
}