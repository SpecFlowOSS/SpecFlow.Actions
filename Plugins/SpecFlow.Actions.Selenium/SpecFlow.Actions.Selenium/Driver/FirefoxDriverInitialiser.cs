using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class FirefoxDriverInitialiser : IDriverInitialiser
    {
        private readonly IOptionsConfigurator _optionsConfigurator;

        private static readonly Lazy<string?> FirefoxWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));

        public FirefoxDriverInitialiser(IOptionsConfigurator optionsConfigurator)
        {
            _optionsConfigurator = optionsConfigurator;
        }

        public IWebDriver Initialise()
        {
            var options = new FirefoxDriverOptions();

            _optionsConfigurator.Add(options);

            return string.IsNullOrWhiteSpace(FirefoxWebDriverFilePath.Value)
                ? new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new FirefoxDriver(FirefoxDriverService.CreateDefaultService(FirefoxWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }
    }
}