using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Browsers;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack.Browsers
{
    public class Edge : IBrowser
    {
        private readonly ScenarioContext _scenarioContext;

        public Edge(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public IWebDriver GetDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new EdgeOptions();

            options.AddAdditionalCapability("browserstack.user", BrowserStack.Username.Value);
            options.AddAdditionalCapability("browserstack.key", BrowserStack.AccessKey.Value);
            options.AddAdditionalCapability("name", BrowserStack.GetTestName(_scenarioContext));

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return new RemoteWebDriver(BrowserStack.RemoteServerUri.Value, options);
        }
    }
}