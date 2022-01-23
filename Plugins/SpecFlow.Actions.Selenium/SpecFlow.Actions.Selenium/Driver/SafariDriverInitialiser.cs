using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace SpecFlow.Actions.Selenium.Driver
{
    internal class SafariDriverInitialiser : IDriverInitialiser
    {
        private readonly IOptionsConfigurator _optionsConfigurator;

        private static readonly Lazy<string?> SafariWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("SAFARI_WEBDRIVER_FILE_PATH"));

        public SafariDriverInitialiser(IOptionsConfigurator optionsConfigurator)
        {
            _optionsConfigurator = optionsConfigurator;
        }

        public IWebDriver Initialise()
        {
            var options = new SafariDriverOptions();

            _optionsConfigurator.Add(options);

            return string.IsNullOrWhiteSpace(SafariWebDriverFilePath.Value)
                ? new SafariDriver(SafariDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new SafariDriver(SafariDriverService.CreateDefaultService(SafariWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }
    }
}