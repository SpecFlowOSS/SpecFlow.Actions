using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using System;
using System.Collections.Generic;

namespace Specflow.Actions.Browserstack
{
    public class BrowserstackDriverInitialiser : IDriverInitialiser
    {
        public IWebDriver GetChromeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            throw new NotImplementedException();
        }

        public IWebDriver GetEdgeDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            throw new NotImplementedException();
        }

        public IWebDriver GetFirefoxDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            throw new NotImplementedException();
        }

        public IWebDriver GetInternetExplorerDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            throw new NotImplementedException();
        }
    }
}
