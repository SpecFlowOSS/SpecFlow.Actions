using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlow.Actions.Selenium
{
    public interface IDriverInitialiser
    {
        IWebDriver GetChromeDriver(Dictionary<string, object>? capabilities = null, string[]? args = null);
        IWebDriver GetEdgeDriver(Dictionary<string, object>? capabilities);
        IWebDriver GetFirefoxDriver(Dictionary<string, object>? capabilities = null, string[]? args = null);
        IWebDriver GetInternetExplorerDriver(Dictionary<string, object>? capabilities);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        private readonly Lazy<string?> _chromeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("CHROME_WEBDRIVER_FILE_PATH"));
        private readonly Lazy<string?> _edgeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));
        private readonly Lazy<string?> _firefoxWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));
        private readonly Lazy<string?> _internetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        //TODO: Should these be internal as to not be called externally by the consuming project???
        /// <summary>
        /// Gets the Chrome driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetChromeDriver(Dictionary<string, object>? capabilities = null, string[]? args = null)
        {
            var options = new ChromeOptions();

            if (capabilities?.Count != 0)
            {
                if (capabilities != null)
                {
                    foreach (var capability in capabilities)
                    {
                        options.AddAdditionalCapability(capability.Key, capability.Value);
                    }
                }
            }

            if (args != null && args?.Length != 0)
            {
                options.AddArguments(args);
            }

            return string.IsNullOrWhiteSpace(_chromeWebDriverFilePath.Value) 
                ? new ChromeDriver(ChromeDriverService.CreateDefaultService(), options) 
                : new ChromeDriver(ChromeDriverService.CreateDefaultService(_chromeWebDriverFilePath.Value), options);
        }

        /// <summary>
        /// Gets the Firefox driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetFirefoxDriver(Dictionary<string, object>? capabilities = null, string[]? args = null)
        {
            var options = new FirefoxOptions();

            if (capabilities?.Count != 0)
            {
                if (capabilities != null)
                {
                    foreach (var capability in capabilities)
                    {
                        options.AddAdditionalCapability(capability.Key, capability.Value);
                    }
                }
            }

            if (args != null && args?.Length != 0)
            {
                options.AddArguments(args);
            }

            return string.IsNullOrWhiteSpace(_firefoxWebDriverFilePath.Value) 
                ? new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options) 
                : new FirefoxDriver(FirefoxDriverService.CreateDefaultService(_firefoxWebDriverFilePath.Value), options);
        }

        /// <summary>
        /// Gets the Internet Explorer driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetInternetExplorerDriver(Dictionary<string, object>? capabilities = null)
        {
            var options = new InternetExplorerOptions();

            if (capabilities?.Count != 0)
            {
                if (capabilities != null)
                {
                    foreach (var capability in capabilities)
                    {
                        options.AddAdditionalCapability(capability.Key, capability.Value);
                    }
                }
            }

            return string.IsNullOrWhiteSpace(_internetExplorerWebDriverFilePath.Value) 
                ? new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options)
                : new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(_internetExplorerWebDriverFilePath.Value), options);
        }

        /// <summary>
        /// Gets the Edge driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetEdgeDriver(Dictionary<string, object>? capabilities = null)
        {
            var options = new EdgeOptions();

            if (capabilities?.Count != 0)
            {
                if (capabilities != null)
                {
                    foreach (var capability in capabilities)
                    {
                        //TODO: make this shit work.
                        options.AddAdditionalCapability(capability.Key, ((string[])capability.Value).ToList());
                    } 
                }
            }

            return string.IsNullOrWhiteSpace(_edgeWebDriverFilePath.Value)
                ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options)
                : new EdgeDriver(EdgeDriverService.CreateDefaultService(_edgeWebDriverFilePath.Value), options);
        }
    }
}