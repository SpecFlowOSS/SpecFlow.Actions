using OpenQA.Selenium;
using SpecFlow.Actions.Browserstack.Browsers;
using SpecFlow.Actions.Selenium.Browsers;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Enums;
using SpecFlow.Actions.Selenium.Factories;
using System;
using TechTalk.SpecFlow;

namespace Specflow.Actions.Browserstack
{
    public class RemoteDriverFactory : IDriverFactory
    {
        private readonly ScenarioContext _scenarioContext;

        public RemoteDriverFactory(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public IWebDriver GetDriver(ITargetConfiguration targetConfiguration)
        {
            IBrowser selectedBrowser = targetConfiguration.Browser switch
            {
                Browser.Chrome => new Chrome(_scenarioContext),
                Browser.Firefox => new Firefox(_scenarioContext),
                Browser.Edge => new Edge(_scenarioContext),
                Browser.InternetExplorer => new InternetExplorer(_scenarioContext),
                Browser.Safari => new Safari(_scenarioContext),
                _ => throw new NotImplementedException($"Support for browser {targetConfiguration.Browser} is not implemented yet"),
            };

            return selectedBrowser.GetDriver(targetConfiguration.Capabilities, targetConfiguration.Arguments);
        }
    }
}