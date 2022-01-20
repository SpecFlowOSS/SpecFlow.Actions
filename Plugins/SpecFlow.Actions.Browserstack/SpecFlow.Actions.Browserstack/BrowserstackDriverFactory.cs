using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Driver;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace SpecFlow.Actions.Browserstack
{
    internal class BrowserstackDriverFactory : IDriverFactory
    {
        private readonly IWebDriverOptionsFactory _driverOptionsFactory;
        private readonly Uri _browserstackRemoteUri;

        public BrowserstackDriverFactory(IWebDriverOptionsFactory driverOptionsFactory)
        {
            _driverOptionsFactory = driverOptionsFactory;
            _browserstackRemoteUri = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
        }

        public IWebDriver GetChromeDriver()
        {
            var options = _driverOptionsFactory.GetChromeOptions();
            return GetDriver(options);
        }

        public IWebDriver GetSafariDriver()
        {
            var options = _driverOptionsFactory.GetSafariOptions();
            return GetDriver(options);
        }

        public IWebDriver GetEdgeDriver()
        {
            var options = _driverOptionsFactory.GetEdgeOptions();
            return GetDriver(options);
        }

        public IWebDriver GetFirefoxDriver()
        {
            var options = _driverOptionsFactory.GetFireFoxOptions();
            return GetDriver(options);
        }

        public IWebDriver GetInternetExplorerDriver()
        {
            var options = _driverOptionsFactory.GetInternetExplorerOptions();
            return GetDriver(options);
        }

        private IWebDriver GetDriver(IOptionsWrapper options)
        {
            return new RemoteWebDriver(_browserstackRemoteUri, options.Value);
        }
    }
}