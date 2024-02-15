using System.Collections.Generic;

namespace Reqnroll.Actions.Appium.Configuration.WindowsAppDriver
{
    public interface IWindowsAppDriverConfiguration
    {
        Dictionary<string, string> Capabilities { get; }

        string? WindowsAppDriverPath { get; }

        int? WindowsAppDriverPort { get; }

        bool? EnableScreenshots { get; }

        bool CloseAppAutomatically { get; }
    }
}