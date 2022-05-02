using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using System;
using System.Collections;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.LambdaTest.DriverInitialisers
{

    internal class LambdaTestChromeDriverInitialiser : ChromeDriverInitialiser
    {
        private readonly ScenarioContext _scenarioContext;
        private static Lazy<string?> Username => new(() => Environment.GetEnvironmentVariable("LT_USERNAME"));
        private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("LT_ACCESS_KEY"));

        private readonly Uri _remoteServer;

        public LambdaTestChromeDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ScenarioContext scenarioContext) : base(seleniumConfiguration)
        {
            _scenarioContext = scenarioContext;
            _remoteServer = new Uri("http://" + Username + ":" + AccessKey + "@hub.lambdatest.com" + "/wd/hub/");
        }

        protected override IWebDriver GetDriver(ChromeOptions options)
        {
            return new RemoteWebDriver(_remoteServer, options);
        }

        protected override ChromeOptions GetChromeOptions()
        {
            var options = base.GetChromeOptions();

            if (Username.Value is not null && AccessKey.Value is not null)
            {
                options.AddAdditionalCapability("username", Username.Value, true);
                options.AddAdditionalCapability("accesskey", AccessKey.Value, true);
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