using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Browserstack;
using SpecFlow.Actions.Selenium;
using System;
using System.Collections.Generic;

namespace Specflow.Actions.Browserstack
{
    public class BrowserstackDriverInitialiser : IDriverInitialiser
    {
        private readonly RemoteWebDriverOptions _remoteWebDriverOptions;
        private Uri _browserstackRemoteUri;

        public BrowserstackDriverInitialiser(RemoteWebDriverOptions remoteWebDriverOptions)
        {
            _remoteWebDriverOptions = remoteWebDriverOptions;
            _browserstackRemoteUri = new("https://hub-cloud.browserstack.com/wd/hub/");
        }

        public IWebDriver Initialise(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var caps = browser switch
            {
                Browser.Chrome => _remoteWebDriverOptions.Chrome(capabilities).ToCapabilities(),
                Browser.Firefox => _remoteWebDriverOptions.Firefox(capabilities).ToCapabilities(),
                Browser.Edge => _remoteWebDriverOptions.Edge(capabilities).ToCapabilities(),
                Browser.InternetExplorer => _remoteWebDriverOptions.InternetExplorer(capabilities).ToCapabilities(),
                Browser.Safari => _remoteWebDriverOptions.Safari(capabilities).ToCapabilities(),
                _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null),
            };

            return new RemoteWebDriver(_browserstackRemoteUri, caps);
        }
    }
}