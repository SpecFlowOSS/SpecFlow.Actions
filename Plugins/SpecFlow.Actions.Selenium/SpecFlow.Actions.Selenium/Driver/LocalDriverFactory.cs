using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class LocalDriverFactory : IDriverFactory
    {
        private readonly IWebDriverOptionsFactory _driverOptionsFactory;

        private static readonly Lazy<string> _chromeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("CHROME_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string> _edgeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string> _firefoxWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string> _internetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        public LocalDriverFactory(IWebDriverOptionsFactory driverOptionsFactory)
        {
            _driverOptionsFactory = driverOptionsFactory;
        }

        /// <summary>
        /// Gets the Chrome driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetChromeDriver()
        {
            var options = _driverOptionsFactory.GetChromeOptions();

            return string.IsNullOrWhiteSpace(_chromeWebDriverFilePath.Value)
                ? new ChromeDriver(ChromeDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new ChromeDriver(ChromeDriverService.CreateDefaultService(_chromeWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Firefox driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetFirefoxDriver()
        {
            var options = _driverOptionsFactory.GetFireFoxOptions();

            return string.IsNullOrWhiteSpace(_firefoxWebDriverFilePath.Value)
                ? new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new FirefoxDriver(FirefoxDriverService.CreateDefaultService(_firefoxWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Internet Explorer driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetInternetExplorerDriver()
        {
            var options = _driverOptionsFactory.GetInternetExplorerOptions();

            return string.IsNullOrWhiteSpace(_internetExplorerWebDriverFilePath.Value)
                ? new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(_internetExplorerWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Edge driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetEdgeDriver()
        {
            var options = _driverOptionsFactory.GetEdgeOptions();

            return string.IsNullOrWhiteSpace(_edgeWebDriverFilePath.Value)
                ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new EdgeDriver(EdgeDriverService.CreateDefaultService(_edgeWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }

        public IWebDriver GetSafariDriver()
        {
            // Unable to test
            throw new NotImplementedException("SafariDriver no implemented for local execution");
        }
    }
}