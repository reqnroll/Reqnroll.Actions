using Reqnroll.Actions.Configuration;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Reqnroll.Actions.Playwright
{
    public interface IPlaywrightConfiguration
    {
        Browser Browser { get; }

        string[]? Arguments { get; }

        float? DefaultTimeout { get; }

        bool? Headless { get; }

        float? SlowMo { get; }
        
        string? TraceDir { get; }
    }

    public class PlaywrightConfiguration : IPlaywrightConfiguration
    {
        private readonly IReqnrollActionJsonLoader _reqnrollActionJsonLoader;

        private class ReqnrollActionJson
        {
            [JsonInclude]
            public PlaywrightReqnrollJsonPart Playwright { get; private set; } = new PlaywrightReqnrollJsonPart();
        }

        private class PlaywrightReqnrollJsonPart
        {
            [JsonInclude]
            public Browser Browser { get; private set; }

            [JsonInclude]
            public string[]? Arguments { get; private set; }

            [JsonInclude]
            public float? DefaultTimeout { get; private set; }

            [JsonInclude]
            public bool? Headless { get; private set; }

            [JsonInclude]
            public float? SlowMo { get; private set; }

            [JsonInclude]
            public string? TraceDir { get; private set; }
        }

        private readonly Lazy<ReqnrollActionJson> _reqnrollJsonPart;

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

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var reqnrollActionConfig = JsonSerializer.Deserialize<ReqnrollActionJson>(json, jsonSerializerOptions);

            return reqnrollActionConfig ?? new ReqnrollActionJson();
        }

        /// <summary>
        /// Provides the configuration details for the Playwright instance
        /// </summary>
        /// <param name="reqnrollActionJsonLoader"></param>
        public PlaywrightConfiguration(IReqnrollActionJsonLoader reqnrollActionJsonLoader)
        {
            _reqnrollActionJsonLoader = reqnrollActionJsonLoader;
            _reqnrollJsonPart = new Lazy<ReqnrollActionJson>(LoadReqnrollJson);
        }

        /// <summary>
        /// The browser specified in the configuration
        /// </summary>
        public Browser Browser => _reqnrollJsonPart.Value.Playwright.Browser;

        /// <summary>
        /// Additional arguments used when launching the browser
        /// </summary>
        public string[]? Arguments => _reqnrollJsonPart.Value.Playwright.Arguments;

        /// <summary>
        /// The default timeout used to configure the browser
        /// </summary>
        public float? DefaultTimeout => _reqnrollJsonPart.Value.Playwright.DefaultTimeout;

        /// <summary>
        /// Whether the browser should launch headless
        /// </summary>
        public bool? Headless => _reqnrollJsonPart.Value.Playwright.Headless;

        /// <summary>
        /// How many miliseconds elapse between every action 
        /// </summary>
        public float? SlowMo => _reqnrollJsonPart.Value.Playwright.SlowMo;

        /// <summary>
        /// If specified, traces are saved into this directory 
        /// </summary>
        public string? TraceDir => _reqnrollJsonPart.Value.Playwright.TraceDir;
    }
}