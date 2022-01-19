using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using SpecFlow.Actions.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack
{
    public class RemoteWebDriverOptions : IDriverOptions
    {
        private readonly ScenarioContext _scenarioContext;
        private static Lazy<string?> BrowserstackUsername => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

        public RemoteWebDriverOptions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public ChromeOptions Chrome(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new ChromeOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value, true);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value, true);
            options.AddAdditionalCapability("name", GetScenarioTitle(), true);

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value, true);
                }
            }

            return options;
        }

        public EdgeOptions Edge(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new EdgeOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value);
            options.AddAdditionalCapability("name", GetScenarioTitle());

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return options;
        }

        public FirefoxOptions Firefox(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new FirefoxOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value, true);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value, true);
            options.AddAdditionalCapability("name", GetScenarioTitle(), true);

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value, true);
                }
            }

            return options;
        }

        public InternetExplorerOptions InternetExplorer(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new InternetExplorerOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value);
            options.AddAdditionalCapability("name", GetScenarioTitle());

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return options;
        }

        public SafariOptions Safari(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new SafariOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value);
            options.AddAdditionalCapability("name", GetScenarioTitle());

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return options;
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