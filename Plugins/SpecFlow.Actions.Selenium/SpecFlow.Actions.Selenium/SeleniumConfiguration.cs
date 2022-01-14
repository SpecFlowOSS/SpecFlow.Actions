using SpecFlow.Actions.Configuration;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public partial class SeleniumConfiguration : ISeleniumConfiguration
    {
        private readonly ISpecFlowActionsConfiguration _specFlowActionsConfiguration;

        /// <summary>
        /// Provides the configuration details for the webdriver instance
        /// </summary>
        /// <param name="specFlowActionJsonLoader"></param>
        public SeleniumConfiguration(ISpecFlowActionsConfiguration specFlowActionsConfiguration)
        {
            _specFlowActionsConfiguration = specFlowActionsConfiguration;
        }

        /// <summary>
        /// The browser specified in the configuration
        /// </summary>
        public Browser Browser => (Browser)Enum.Parse(typeof(Browser), _specFlowActionsConfiguration.Get("selenium:browser", "None"), true);

        /// <summary>
        /// Arguments used to configure the webdriver
        /// </summary>
        public string[] Arguments => _specFlowActionsConfiguration.GetArray("selenium:arguments") ?? new string[]{};

        /// <summary>
        /// Capabilities used to configure the webdriver
        /// </summary>
        public Dictionary<string, string> Capabilities =>
            _specFlowActionsConfiguration.GetDictionary("selenium:capabilities");

        /// <summary>
        /// The default timeout used to configure the webdriver
        /// </summary>
        public double? DefaultTimeout => _specFlowActionsConfiguration.GetDouble("selenium:defaulttimeout");

        /// <summary>
        /// The default polling interval used to configure the webdriver
        /// </summary>
        public double? PollingInterval => _specFlowActionsConfiguration.GetDouble("selenium:pollinginterval");

        /// <summary>
        /// The test platform to execute against
        /// </summary>
        public string TestPlatform => _specFlowActionsConfiguration.Get("selenium:testplatform", "local");
    }
}