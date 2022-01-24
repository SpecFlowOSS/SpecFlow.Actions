using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SpecFlow.Actions.Selenium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class EdgeDriverInitialiser : IDriverInitialiser
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private static readonly Lazy<string?> EdgeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));

        public EdgeDriverInitialiser(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public IWebDriver Initialise()
        {
            var options = GetEdgeOptions();

            return GetWebDriver(options);
        }

        protected virtual EdgeOptions GetEdgeOptions()
        {
            var options = new EdgeOptions();

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

        protected virtual IWebDriver GetWebDriver(EdgeOptions options)
        {
            return string.IsNullOrWhiteSpace(EdgeWebDriverFilePath.Value)
                ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new EdgeDriver(EdgeDriverService.CreateDefaultService(EdgeWebDriverFilePath.Value), options,
                    TimeSpan.FromSeconds(120));
        }
    }
}