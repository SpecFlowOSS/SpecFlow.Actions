using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Driver;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;

namespace Specflow.Actions.Browserstack;

internal class BrowserstackDriverInitialiser: IDriverInitialiser
{
    private readonly IOptionsConfigurator _optionsConfigurator;
    private readonly IOptionsWrapper _optionsWrapper;
    private readonly Uri _browserstackRemoteServer;

    private static Lazy<string?> BrowserstackUsername => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
    private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

    public BrowserstackDriverInitialiser(IOptionsConfigurator optionsConfigurator, IOptionsWrapper optionsWrapper)
    {
        _optionsConfigurator = optionsConfigurator;
        _optionsWrapper = optionsWrapper;
        _browserstackRemoteServer = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
    }

    public IWebDriver Initialise()
    {
        _optionsConfigurator.Add(_optionsWrapper);

        return new RemoteWebDriver(_browserstackRemoteServer, _optionsWrapper.Value);
    }
}