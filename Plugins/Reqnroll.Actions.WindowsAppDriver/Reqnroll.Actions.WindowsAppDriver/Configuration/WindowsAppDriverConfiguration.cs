using Reqnroll.Actions.Appium.Configuration.WindowsAppDriver;
using Reqnroll.Actions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Reqnroll.Actions.WindowsAppDriver.Configuration
{
    public class WindowsAppDriverConfiguration : IWindowsAppDriverConfiguration
    {
        private readonly IReqnrollActionJsonLoader _reqnrollActionJsonLoader;
        private readonly Lazy<ReqnrollActionJson> _reqnrollJsonPart;

        public WindowsAppDriverConfiguration(IReqnrollActionJsonLoader reqnrollActionJsonLoader)
        {
            _reqnrollActionJsonLoader = reqnrollActionJsonLoader;
            _reqnrollJsonPart = new Lazy<ReqnrollActionJson>(LoadReqnrollJson);
        }

        private ReqnrollActionJson LoadReqnrollJson()
        {
            var json = _reqnrollActionJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new ReqnrollActionJson();
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var reqnrollActionConfig = JsonSerializer.Deserialize<ReqnrollActionJson>(json, jsonSerializerOptions);

            return reqnrollActionConfig ?? new ReqnrollActionJson();
        }

        public Dictionary<string, string> Capabilities => _reqnrollJsonPart.Value.WindowsAppDriver.Capabilities;

        public string? WindowsAppDriverPath => _reqnrollJsonPart.Value.WindowsAppDriver.WindowsAppDriverPath;

        public bool? EnableScreenshots => _reqnrollJsonPart.Value.WindowsAppDriver.EnableScreenshots;

        public int? WindowsAppDriverPort => _reqnrollJsonPart.Value.WindowsAppDriver.WindowsAppDriverPort;

        public bool CloseAppAutomatically => _reqnrollJsonPart.Value.WindowsAppDriver.CloseAppAutomatically ?? true;
    }
}