using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using System.Collections;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack
{
    internal class BrowserstackOptionsConfigurator : IOptionsConfigurator
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly ScenarioContext _scenarioContext;

        private static Lazy<string?> BrowserstackUsername => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

        public BrowserstackOptionsConfigurator(ISeleniumConfiguration seleniumConfiguration, ScenarioContext scenarioContext)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _scenarioContext = scenarioContext;
        }

        public void Add(IOptionsWrapper options)
        {
            if (BrowserstackUsername.Value is not null && AccessKey.Value is not null)
            {
                options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
                options.AddAdditionalCapability("browserstack.key", AccessKey.Value);
            }

            options.AddAdditionalCapability("name", GetScenarioTitle());

            if (_seleniumConfiguration.Capabilities.Any() && _seleniumConfiguration.Capabilities is not null)
            {
                foreach (var capability in _seleniumConfiguration.Capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }
        }

        private string GetScenarioTitle()
        {
            var testName = _scenarioContext.ScenarioInfo.Title;

            if (_scenarioContext.ScenarioInfo.Arguments.Count > 0)
            {
                testName += ": ";
            }

            foreach (DictionaryEntry argument in _scenarioContext.ScenarioInfo.Arguments)
            {
                testName += argument.Key + ":" + argument.Value + "; ";
            }

            return testName.Trim();
        }
    }
}