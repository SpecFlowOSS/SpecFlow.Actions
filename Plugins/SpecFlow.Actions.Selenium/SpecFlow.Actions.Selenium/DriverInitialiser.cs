using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface IDriverInitialiser
    {
        IWebDriver GetChromeDriver(string[]? args);
        IWebDriver GetEdgeDriver(Dictionary<string, object>? capabilities, string[]? args = null);
        IWebDriver GetFirefoxDriver(string[]? args);
        IWebDriver GetInternetExplorerDriver(Dictionary<string, object>? capabilities, string[]? args = null);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        private readonly Lazy<string?> _chromeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("CHROME_WEBDRIVER_FILE_PATH"));
        private readonly Lazy<string?> _edgeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH", EnvironmentVariableTarget.User));
        private readonly Lazy<string?> _firefoxWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("FIREFOX_WEBDRIVER_FILE_PATH"));
        private readonly Lazy<string?> _internetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        public IWebDriver GetChromeDriver(string[]? args = null)
        {
            var options = new ChromeOptions();

            if (args != null && args?.Length != 0)
            {
                options.AddArguments(args);
            }

            return new ChromeDriver(string.IsNullOrWhiteSpace(_chromeWebDriverFilePath.Value) ?
                ChromeDriverService.CreateDefaultService() : ChromeDriverService.CreateDefaultService(_chromeWebDriverFilePath.Value), options);
        }

        public IWebDriver GetFirefoxDriver(string[]? args = null)
        {
            var options = new FirefoxOptions();

            if (args != null && args?.Length != 0)
            {
                options.AddArguments(args);
            }

            return new FirefoxDriver(string.IsNullOrWhiteSpace(_firefoxWebDriverFilePath.Value) ?
                FirefoxDriverService.CreateDefaultService() : FirefoxDriverService.CreateDefaultService(_firefoxWebDriverFilePath.Value), options);
        }

        public IWebDriver GetInternetExplorerDriver(Dictionary<string, object>? capabilities = null, string[]? args = null)
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

            if (args != null && args?.Length != 0)
            {
                options.AddAdditionalCapability("args", args);
            }

            return new InternetExplorerDriver(string.IsNullOrWhiteSpace(_internetExplorerWebDriverFilePath.Value) ?
                InternetExplorerDriverService.CreateDefaultService() : InternetExplorerDriverService.CreateDefaultService(_internetExplorerWebDriverFilePath.Value), options);
        }

        public IWebDriver GetEdgeDriver(Dictionary<string, object>? capabilities = null, string[]? args = null)
        {
            var options = new EdgeOptions();

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
                options.AddAdditionalCapability("args", args);
            }

            return new EdgeDriver(string.IsNullOrWhiteSpace(_edgeWebDriverFilePath.Value) ?
                EdgeDriverService.CreateDefaultService() : EdgeDriverService.CreateDefaultService(_edgeWebDriverFilePath.Value), options);
        }
    }
}