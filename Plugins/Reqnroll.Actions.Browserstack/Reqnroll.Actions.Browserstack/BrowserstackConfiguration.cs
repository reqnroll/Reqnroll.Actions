using Reqnroll.Actions.Configuration;
using Reqnroll.Actions.Selenium.Configuration;
using System.Collections.Generic;

namespace Reqnroll.Actions.Browserstack
{
    public class BrowserstackConfiguration : SeleniumConfiguration
    {
        private readonly IReqnrollActionsConfiguration _reqnrollActionsConfiguration;

        public BrowserstackConfiguration(IReqnrollActionsConfiguration reqnrollActionsConfiguration) : base(reqnrollActionsConfiguration)
        {
            _reqnrollActionsConfiguration = reqnrollActionsConfiguration;
        }

        public Dictionary<string, string> BrowserstackLocalCapabilities =>
            _reqnrollActionsConfiguration.GetDictionary("selenium:browserstack:localcapabilities");

        public bool BrowserstackLocalRequired
        {
            get
            {
                if (base.Capabilities.ContainsKey("browserstack.local"))
                {
                    return base.Capabilities["browserstack.local"] == "true";
                }

                return false;
            }
        }


        public string Url =>
            _reqnrollActionsConfiguration.Get("selenium:browserstack:url",
                "https://hub-cloud.browserstack.com/wd/hub/");

    }
}