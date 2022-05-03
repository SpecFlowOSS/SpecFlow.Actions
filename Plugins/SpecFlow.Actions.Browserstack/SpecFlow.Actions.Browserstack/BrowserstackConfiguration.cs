using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Selenium.Configuration;
using System.Collections.Generic;

namespace SpecFlow.Actions.Browserstack
{
    public class BrowserstackConfiguration : SeleniumConfiguration
    {
        private readonly ISpecFlowActionsConfiguration _specFlowActionsConfiguration;

        public BrowserstackConfiguration(ISpecFlowActionsConfiguration specFlowActionsConfiguration) : base(specFlowActionsConfiguration)
        {
            _specFlowActionsConfiguration = specFlowActionsConfiguration;
        }

        public Dictionary<string, string> BrowserstackLocalCapabilities =>
            _specFlowActionsConfiguration.GetDictionary("selenium:browserstack:localcapabilities");

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
            _specFlowActionsConfiguration.Get("selenium:browserstack:url",
                "https://hub-cloud.browserstack.com/wd/hub/");

    }
}