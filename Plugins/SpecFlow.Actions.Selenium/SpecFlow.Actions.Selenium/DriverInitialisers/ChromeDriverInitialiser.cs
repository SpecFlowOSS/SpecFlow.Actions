using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlow.Actions.Selenium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Selenium.DriverInitialisers
{
    public class ChromeDriverInitialiser : IDriverInitialiser
    {
        private readonly ISeleniumConfiguration _browserstackConfiguration;
        private static readonly Lazy<string?> ChromeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("CHROME_WEBDRIVER_FILE_PATH"));

        public ChromeDriverInitialiser(ISeleniumConfiguration browserstackConfiguration)
        {
            _browserstackConfiguration = browserstackConfiguration;
        }

        public IWebDriver Initialise()
        {
            var options = GetChromeOptions();
            return GetWebDriver(options);
        }

        protected virtual ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();

            if (_browserstackConfiguration.Capabilities.Any())
            {
                foreach (var capability in _browserstackConfiguration.Capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value, true);
                }
            }

            if (_browserstackConfiguration.Arguments.Any())
            {
                options.AddArguments(_browserstackConfiguration.Arguments);
            }

            return options;
        }

        protected virtual IWebDriver GetWebDriver(ChromeOptions options)
        {
            return string.IsNullOrWhiteSpace(ChromeWebDriverFilePath.Value)
                ? new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new ChromeDriver(ChromeDriverService.CreateDefaultService(ChromeWebDriverFilePath.Value), options,
                    TimeSpan.FromSeconds(120));
        }
    }
}