using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers
{
    internal class BrowserstackInternetExplorerDriverInitialiser : InternetExplorerDriverInitialiser
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Uri _browserstackRemoteServer;

        private static Lazy<string?> BrowserstackUsername => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

        public BrowserstackInternetExplorerDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ScenarioContext scenarioContext) : base(seleniumConfiguration)
        {
            _scenarioContext = scenarioContext;
            _browserstackRemoteServer = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
        }

        protected override InternetExplorerOptions GetInternetExplorerOptions()
        {
            var options = base.GetInternetExplorerOptions();

            if (BrowserstackUsername.Value is not null && AccessKey.Value is not null)
            {
                options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
                options.AddAdditionalCapability("browserstack.key", AccessKey.Value);
            }

            options.AddAdditionalCapability("name", GetScenarioTitle());

            return options;
        }

        protected override IWebDriver GetDriver(InternetExplorerOptions options)
        {
            return new RemoteWebDriver(_browserstackRemoteServer, options);
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
