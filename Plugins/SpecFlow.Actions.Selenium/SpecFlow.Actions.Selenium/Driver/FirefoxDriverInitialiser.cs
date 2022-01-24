using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SpecFlow.Actions.Selenium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class FirefoxDriverInitialiser : IDriverInitialiser
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private static readonly Lazy<string?> FirefoxWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));

        public FirefoxDriverInitialiser(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public IWebDriver Initialise()
        {
            var options = GetFirefoxOptions();
            return GetWebDriver(options);
        }

        protected virtual FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions();

            if (_seleniumConfiguration.Capabilities.Any())
            {
                foreach (var capability in _seleniumConfiguration.Capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value, true);
                }
            }

            if (_seleniumConfiguration.Arguments.Any())
            {
                options.AddArguments(_seleniumConfiguration.Arguments);
            }

            return options;
        }

        protected virtual IWebDriver GetWebDriver(FirefoxOptions options)
        {
            return string.IsNullOrWhiteSpace(FirefoxWebDriverFilePath.Value)
                ? new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new FirefoxDriver(FirefoxDriverService.CreateDefaultService(FirefoxWebDriverFilePath.Value), options,
                    TimeSpan.FromSeconds(120));
        }
    }
}