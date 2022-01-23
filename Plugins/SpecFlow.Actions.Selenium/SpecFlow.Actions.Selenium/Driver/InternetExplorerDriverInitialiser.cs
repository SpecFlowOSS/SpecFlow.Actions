using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class InternetExplorerDriverInitialiser : IDriverInitialiser
    {
        private readonly IOptionsConfigurator _optionsConfigurator;

        private static readonly Lazy<string?> InternetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        public InternetExplorerDriverInitialiser(IOptionsConfigurator optionsConfigurator)
        {
            _optionsConfigurator = optionsConfigurator;
        }

        public IWebDriver Initialise()
        {
            var options = new InternetExplorerDriverOptions();

            _optionsConfigurator.Add(options);

            return string.IsNullOrWhiteSpace(InternetExplorerWebDriverFilePath.Value)
                ? new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(InternetExplorerWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }
    }
}