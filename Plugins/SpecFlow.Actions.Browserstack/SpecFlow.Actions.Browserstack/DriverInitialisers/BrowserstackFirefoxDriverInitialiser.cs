﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using System;
using System.Collections;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers
{
    internal class BrowserstackFirefoxDriverInitialiser : FirefoxDriverInitialiser
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly ScenarioContext _scenarioContext;
        private readonly Uri _browserstackRemoteServer;

        private static Lazy<string?> BrowserstackUsername => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

        public BrowserstackFirefoxDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ScenarioContext scenarioContext) : base(seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _scenarioContext = scenarioContext;
            _browserstackRemoteServer = new Uri("https://hub-cloud.browserstack.com/wd/hub/");

            if (((BrowserstackConfiguration)_seleniumConfiguration).BrowserstackLocalRequired)
            {
                StartBrowserstackLocal();
            }
        }

        private void StartBrowserstackLocal()
        {
            BrowserstackLocalService.Start(
                ((BrowserstackConfiguration)_seleniumConfiguration).BrowserstackLocalCapabilities.ToList());
        }

        protected override IWebDriver GetWebDriver(FirefoxOptions options)
        {
            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }

        protected override FirefoxOptions GetFirefoxOptions()
        {
            var options = base.GetFirefoxOptions();

            if (BrowserstackUsername.Value is not null && AccessKey.Value is not null)
            {
                options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value, true);
                options.AddAdditionalCapability("browserstack.key", AccessKey.Value, true);
            }

            options.AddAdditionalCapability("name", GetScenarioTitle(), true);

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