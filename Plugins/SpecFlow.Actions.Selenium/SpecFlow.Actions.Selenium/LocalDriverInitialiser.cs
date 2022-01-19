using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public class LocalDriverInitialiser : IDriverInitialiser
    {
        private readonly IDriverOptions _driverOptions;
        private static readonly Lazy<string?> _chromeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("CHROME_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string?> _edgeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string?> _firefoxWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));
        private static readonly Lazy<string?> _internetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        public LocalDriverInitialiser(IDriverOptions driverOptions)
        {
            _driverOptions = driverOptions;
        }

        public IWebDriver Initialise(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            return browser switch
            {
                Browser.Chrome => GetChromeDriver(capabilities, args),
                Browser.Firefox => GetFirefoxDriver(capabilities, args),
                Browser.Edge => GetEdgeDriver(capabilities, args),
                Browser.InternetExplorer => GetInternetExplorerDriver(),
                _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null),
            };
        }

        /// <summary>
        /// Gets the Chrome driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        private IWebDriver GetChromeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = _driverOptions.Chrome(capabilities, args);

            return string.IsNullOrWhiteSpace(_chromeWebDriverFilePath.Value)
                ? new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new ChromeDriver(ChromeDriverService.CreateDefaultService(_chromeWebDriverFilePath.Value), options, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Firefox driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        private IWebDriver GetFirefoxDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = _driverOptions.Firefox(capabilities, args);

            return string.IsNullOrWhiteSpace(_firefoxWebDriverFilePath.Value)
                ? new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new FirefoxDriver(FirefoxDriverService.CreateDefaultService(_firefoxWebDriverFilePath.Value), options, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Internet Explorer driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        private IWebDriver GetInternetExplorerDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = _driverOptions.InternetExplorer(capabilities, args);

            return string.IsNullOrWhiteSpace(_internetExplorerWebDriverFilePath.Value)
                ? new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(_internetExplorerWebDriverFilePath.Value), options, TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Gets the Edge driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        private IWebDriver GetEdgeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = _driverOptions.Edge(capabilities, args);

            return string.IsNullOrWhiteSpace(_edgeWebDriverFilePath.Value)
                ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
                : new EdgeDriver(EdgeDriverService.CreateDefaultService(_edgeWebDriverFilePath.Value), options, TimeSpan.FromSeconds(120));
        }

        private IWebDriver GetSafariDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            // Can't be tested without a mac.
            throw new NotImplementedException();
        }
    }
}
