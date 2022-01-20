using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public class DriverInitialiser : IDriverInitialiser
    {
        private readonly IWebDriverOptions _driverOptions;
        private readonly IDriverFactory _driverFactory;

        public DriverInitialiser(WebDriverOptions driverOptions, IDriverFactory driverFactory)
        {
            _driverOptions = driverOptions;
            _driverFactory = driverFactory;
        }

        public IWebDriver Initialise(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = _driverOptions.GetOptions(browser, capabilities, args);

            return browser switch
            {
                Browser.Chrome => _driverFactory.GetChromeDriver(options),
                Browser.Firefox => _driverFactory.GetFirefoxDriver(options),
                Browser.Edge => _driverFactory.GetEdgeDriver(options),
                Browser.InternetExplorer => _driverFactory.GetInternetExplorerDriver(options),
                _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null)
            };
        }
    }
}