using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Browsers;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack.Browsers
{
    public class Firefox : IBrowser
    {
        private readonly ScenarioContext _scenarioContext;

        public Firefox(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public IWebDriver GetDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new FirefoxOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserStack.Username.Value, true);
            options.AddAdditionalCapability("browserstack.key", BrowserStack.AccessKey.Value, true);
            options.AddAdditionalCapability("name", BrowserStack.GetTestName(_scenarioContext), true);

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value, true);
                }
            }

            return new RemoteWebDriver(BrowserStack.RemoteServerUri.Value, options);
        }
    }
}