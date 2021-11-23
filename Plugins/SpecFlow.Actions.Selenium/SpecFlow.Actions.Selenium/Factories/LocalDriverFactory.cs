using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Browsers;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Enums;
using System;

namespace SpecFlow.Actions.Selenium.Factories
{
    public class LocalDriverFactory : IDriverFactory
    {
        public IWebDriver GetDriver(ITargetConfiguration targetConfiguration)
        {
            IBrowser selectedBrowser = targetConfiguration.Browser switch
            {
                Browser.Chrome => new Chrome(),
                Browser.Firefox => new Firefox(),
                Browser.Edge => new Edge(),
                Browser.InternetExplorer => new InternetExplorer(),
                Browser.Safari => new Safari(),
                Browser.Noop => new Noop(),
                _ => throw new NotImplementedException($"Support for browser {targetConfiguration.Browser} is not implemented yet"),
            };
            return selectedBrowser.GetDriver(targetConfiguration.Capabilities, targetConfiguration.Arguments);
        }
    }
}