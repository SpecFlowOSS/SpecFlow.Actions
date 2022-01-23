using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Driver;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace Specflow.Actions.Browserstack
{

    internal class BrowserstackDriverInitialiser : IDriverInitialiser
    {
        private readonly IOptionsConfigurator _optionsConfigurator;
        private readonly IDriverOptions _options;
        private readonly Uri _browserstackRemoteServer;

        public BrowserstackDriverInitialiser(IOptionsConfigurator optionsConfigurator, IDriverOptions options)
        {
            _optionsConfigurator = optionsConfigurator;
            _options = options;
            _browserstackRemoteServer = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
        }

        public IWebDriver Initialise()
        {
            _optionsConfigurator.Add(_options);

            return new RemoteWebDriver(_browserstackRemoteServer, _options.Value);
        }
    }
}