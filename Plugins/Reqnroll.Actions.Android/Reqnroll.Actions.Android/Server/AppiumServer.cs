using OpenQA.Selenium.Appium.Service;
using Reqnroll.Actions.Appium.Server;
using System;

namespace Reqnroll.Actions.Android.Server
{
    internal class AppiumServer : IAppiumServer
    {
        private readonly Lazy<AppiumLocalService> _appiumLocalServiceLazy = new(new AppiumServiceBuilder().UsingAnyFreePort().Build);

        public AppiumLocalService Current => _appiumLocalServiceLazy.Value;

        public void Dispose()
        {
            if (Current.IsRunning)
            {
                Current.Dispose();
            }
        }
    }
}