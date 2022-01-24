using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using SpecFlow.Actions.Selenium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class SafariDriverInitialiser : IDriverInitialiser
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private static readonly Lazy<string?> SafariWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("SAFARI_WEBDRIVER_FILE_PATH"));

        public SafariDriverInitialiser(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public IWebDriver Initialise()
        {
            var options = GetSafariOptions();

            return GetDriver(options);
        }

        protected virtual IWebDriver GetDriver(SafariOptions options)
        {
            return string.IsNullOrWhiteSpace(SafariWebDriverFilePath.Value)
                ? new SafariDriver(SafariDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new SafariDriver(SafariDriverService.CreateDefaultService(SafariWebDriverFilePath.Value), options,
                    TimeSpan.FromSeconds(120));
        }

        protected virtual SafariOptions GetSafariOptions()
        {
            var options = new SafariOptions();

            if (_seleniumConfiguration.Capabilities.Any())
            {
                foreach (var capability in _seleniumConfiguration.Capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (_seleniumConfiguration.Arguments.Any())
            {
                options.AddAdditionalCapability("args", _seleniumConfiguration.Arguments.ToList());
            }

            return options;
        }
    }
}