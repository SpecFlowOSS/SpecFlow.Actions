using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.LambdaTest.DriverInitialisers
{
    internal class LambdaTestSafariDriverInitialiser : SafariDriverInitialiser
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Uri _remoteServer;

        private static Lazy<string?> Username => new(() => Environment.GetEnvironmentVariable("LT_USERNAME"));
        private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("LT_ACCESS_KEY"));

        public LambdaTestSafariDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ScenarioContext scenarioContext) : base(seleniumConfiguration)
        {
            _scenarioContext = scenarioContext;
            _remoteServer = new Uri("http://" + Username + ":" + AccessKey + "@hub.lambdatest.com" + "/wd/hub/");
        }

        protected override IWebDriver GetDriver(SafariOptions options)
        {
            return new RemoteWebDriver(_remoteServer, options);
        }

        protected override SafariOptions GetSafariOptions()
        {
            var options = base.GetSafariOptions();

            if (Username.Value is not null && AccessKey.Value is not null)
            {
                options.AddAdditionalCapability("username", Username.Value);
                options.AddAdditionalCapability("accesskey", AccessKey.Value);
            }

            options.AddAdditionalCapability("name", GetScenarioTitle());

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