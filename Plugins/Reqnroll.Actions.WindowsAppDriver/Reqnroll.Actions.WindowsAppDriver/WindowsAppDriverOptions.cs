using OpenQA.Selenium.Appium;
using Reqnroll.Actions.Appium.Configuration.WindowsAppDriver;
using Reqnroll.Actions.Appium.Driver;
using System;
using System.IO;

namespace Reqnroll.Actions.WindowsAppDriver
{
    internal class WindowsAppDriverOptions : IDriverOptions
    {
        private readonly IWindowsAppDriverConfiguration _windowsAppDriverConfiguration;

        public AppiumOptions Current => GetOptions();

        internal WindowsAppDriverOptions(IWindowsAppDriverConfiguration windowsAppDriverConfiguration)
        {
            _windowsAppDriverConfiguration = windowsAppDriverConfiguration;
        }

        private AppiumOptions GetOptions()
        {
            var options = new AppiumOptions();

            var appFilePath = Environment.GetEnvironmentVariable("TEST_SUBJECT_FILE_PATH");

            if (appFilePath != null)
            {
                options.AddAdditionalCapability("app", appFilePath);
            }

            foreach (var capability in _windowsAppDriverConfiguration.Capabilities)
            {
                if (string.Equals(capability.Key, "app", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(capability.Key, "appWorkingDir", StringComparison.OrdinalIgnoreCase))
                {
                    options.AddAdditionalCapability(capability.Key, Path.Combine(Directory.GetCurrentDirectory(), capability.Value));
                }
                else
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return options;
        }
    }
}