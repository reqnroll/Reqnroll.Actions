using Reqnroll.Actions.Appium.Configuration.Android;
using Reqnroll.Actions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Reqnroll.Actions.Android.Configuration
{
    public class AndroidConfiguration : IAndroidConfiguration
    {
        private readonly IReqnrollActionJsonLoader _reqnrollActionJsonLoader;
        private readonly Lazy<ReqnrollActionJson> _reqnrollJsonPart;

        public AndroidConfiguration(IReqnrollActionJsonLoader reqnrollActionJsonLoader)
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

        public Dictionary<string, string> Capabilities => _reqnrollJsonPart.Value.Android.Capabilities;

        public int? Timeout => _reqnrollJsonPart.Value.Android.Timeout;

        public bool LocalAppiumServerRequired => _reqnrollJsonPart.Value.AppiumServer.LocalAppiumServerRequired ?? true;

        public Uri? ServerAddress => _reqnrollJsonPart.Value.AppiumServer.ServerAddress;
    }
}