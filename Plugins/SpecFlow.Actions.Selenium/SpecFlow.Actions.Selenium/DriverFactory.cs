using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace SpecFlow.Actions.Selenium
{
    public class DriverFactory : IDriverFactory
    {
        private static readonly Lazy<string> _chromeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("CHROME_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string> _edgeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string> _firefoxWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string> _internetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        /// <summary>
        /// Gets the Chrome driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetChromeDriver(IOptionsWrapper options)
        {
            return string.IsNullOrWhiteSpace(_chromeWebDriverFilePath.Value)
                ? new ChromeDriver(ChromeDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new ChromeDriver(ChromeDriverService.CreateDefaultService(_chromeWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Firefox driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetFirefoxDriver(IOptionsWrapper options)
        {
            return string.IsNullOrWhiteSpace(_firefoxWebDriverFilePath.Value)
                ? new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new FirefoxDriver(FirefoxDriverService.CreateDefaultService(_firefoxWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Internet Explorer driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetInternetExplorerDriver(IOptionsWrapper options)
        {
            return string.IsNullOrWhiteSpace(_internetExplorerWebDriverFilePath.Value)
                ? new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(_internetExplorerWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Edge driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetEdgeDriver(IOptionsWrapper options)
        {
            return string.IsNullOrWhiteSpace(_edgeWebDriverFilePath.Value)
                ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
                : new EdgeDriver(EdgeDriverService.CreateDefaultService(_edgeWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
        }
    }
}