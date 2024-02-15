using OpenQA.Selenium.Appium.Service;
using System;

namespace Reqnroll.Actions.Appium.Server
{
    public interface IAppiumServer : IDisposable
    {
        AppiumLocalService Current { get; }
    }
}