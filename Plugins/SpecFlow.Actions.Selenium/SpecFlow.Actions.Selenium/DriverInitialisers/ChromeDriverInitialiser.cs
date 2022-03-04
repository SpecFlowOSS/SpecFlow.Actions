using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlow.Actions.Selenium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Selenium.DriverInitialisers
{
    public class ChromeDriverInitialiser : IDriverInitialiser
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private static readonly Lazy<string?> ChromeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("CHROME_WEBDRIVER_FILE_PATH"));

        public ChromeDriverInitialiser(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public IWebDriver Initialise()
        {
            var options = GetChromeOptions();
            return GetDriver(options);
        }

        protected virtual ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();

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

        protected virtual IWebDriver GetDriver(ChromeOptions options)
        {
            return string.IsNullOrWhiteSpace(ChromeWebDriverFilePath.Value)
                ? new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new ChromeDriver(ChromeDriverService.CreateDefaultService(ChromeWebDriverFilePath.Value), options,
                    TimeSpan.FromSeconds(120));
        }
    }
}