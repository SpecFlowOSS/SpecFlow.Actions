using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Specflow.Actions.Browserstack
{
    public class BrowserstackDriverInitialiser : IDriverInitialiser
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Uri _browserstackRemoteServer;

        private static Lazy<string> BrowserstackUsername => new (() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        private static Lazy<string> AccessKey => new (() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

        public BrowserstackDriverInitialiser(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _browserstackRemoteServer = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
        }

        public IWebDriver GetChromeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new ChromeOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value, true);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value, true);
            options.AddAdditionalCapability("name", _scenarioContext.ScenarioInfo.Title, true);

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability1 in capabilities)
                {
                    options.AddAdditionalCapability(capability1.Key, capability1.Value, true);
                }
            }

            var capability = options;

            return new RemoteWebDriver(_browserstackRemoteServer, capability);
        }

        public IWebDriver GetEdgeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new EdgeOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value);
            options.AddAdditionalCapability("name", _scenarioContext.ScenarioInfo.Title);

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }
            

            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }

        public IWebDriver GetFirefoxDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new FirefoxOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value, true);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value, true);
            options.AddAdditionalCapability("name", _scenarioContext.ScenarioInfo.Title, true);

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value, true);
                }
            }
            

            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }

        public IWebDriver GetInternetExplorerDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new InternetExplorerOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
            options.AddAdditionalCapability("browserstack.key", AccessKey.Value);
            options.AddAdditionalCapability("name", _scenarioContext.ScenarioInfo.Title);

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }
            

            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }
    }
}