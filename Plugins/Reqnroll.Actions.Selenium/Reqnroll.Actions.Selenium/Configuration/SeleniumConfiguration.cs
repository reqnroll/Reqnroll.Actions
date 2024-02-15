using Reqnroll.Actions.Configuration;
using System;
using System.Collections.Generic;

namespace Reqnroll.Actions.Selenium.Configuration
{
    public class SeleniumConfiguration : ISeleniumConfiguration
    {
        private readonly IReqnrollActionsConfiguration _reqnrollActionsConfiguration;

        /// <summary>
        /// Provides the configuration details for the webdriver instance
        /// </summary>
        /// <param name="reqnrollActionJsonLoader"></param>
        public SeleniumConfiguration(IReqnrollActionsConfiguration reqnrollActionsConfiguration)
        {
            _reqnrollActionsConfiguration = reqnrollActionsConfiguration;
        }

        /// <summary>
        /// The browser specified in the configuration
        /// </summary>
        public Browser Browser => (Browser)Enum.Parse(typeof(Browser), _reqnrollActionsConfiguration.Get("selenium:browser", "None"), true);

        /// <summary>
        /// Arguments used to configure the webdriver
        /// </summary>
        public string[] Arguments => _reqnrollActionsConfiguration.GetArray("selenium:arguments") ?? new string[] { };

        /// <summary>
        /// Capabilities used to configure the webdriver
        /// </summary>
        public Dictionary<string, string> Capabilities =>
            _reqnrollActionsConfiguration.GetDictionary("selenium:capabilities");

        /// <summary>
        /// The default timeout used to configure the webdriver
        /// </summary>
        public double? DefaultTimeout => _reqnrollActionsConfiguration.GetDouble("selenium:defaulttimeout");

        /// <summary>
        /// The default polling interval used to configure the webdriver
        /// </summary>
        public double? PollingInterval => _reqnrollActionsConfiguration.GetDouble("selenium:pollinginterval");
    }
}