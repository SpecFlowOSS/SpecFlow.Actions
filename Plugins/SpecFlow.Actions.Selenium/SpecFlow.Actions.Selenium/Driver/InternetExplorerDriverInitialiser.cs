using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using SpecFlow.Actions.Selenium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class InternetExplorerDriverInitialiser : IDriverInitialiser
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private static readonly Lazy<string?> InternetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        public InternetExplorerDriverInitialiser(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public IWebDriver Initialise()
        {
            var options = GetInternetExplorerOptions();

            return GetDriver(options);
        }

        protected virtual InternetExplorerOptions GetInternetExplorerOptions()
        {
            var options = new InternetExplorerOptions();

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

        protected virtual IWebDriver GetDriver(InternetExplorerOptions options)
        {
            return string.IsNullOrWhiteSpace(InternetExplorerWebDriverFilePath.Value)
                ? new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options,
                    TimeSpan.FromSeconds(120))
                : new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(InternetExplorerWebDriverFilePath.Value), options,
                    TimeSpan.FromSeconds(120));
        }
    }
}