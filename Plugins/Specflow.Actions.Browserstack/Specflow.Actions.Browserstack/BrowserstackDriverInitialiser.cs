using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium;
using System;
using System.Collections.Generic;

namespace Specflow.Actions.Browserstack
{
    public class BrowserstackDriverInitialiser : IDriverInitialiser
    {
        private readonly Uri _browserstackRemoteServer;
        private const string BrowserstackOptions = "bstack:option";

        public BrowserstackDriverInitialiser()
        {
            _browserstackRemoteServer = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
        }

        public IWebDriver GetChromeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new ChromeOptions();

            options.AddAdditionalCapability(BrowserstackOptions, capabilities);

            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }

        public IWebDriver GetEdgeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new EdgeOptions();

            options.AddAdditionalCapability(BrowserstackOptions, capabilities);

            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }

        public IWebDriver GetFirefoxDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new FirefoxOptions();

            options.AddAdditionalCapability(BrowserstackOptions, capabilities);

            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }

        public IWebDriver GetInternetExplorerDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new InternetExplorerOptions();

            options.AddAdditionalCapability(BrowserstackOptions, capabilities);

            return new RemoteWebDriver(_browserstackRemoteServer, options);
        }
    }
}