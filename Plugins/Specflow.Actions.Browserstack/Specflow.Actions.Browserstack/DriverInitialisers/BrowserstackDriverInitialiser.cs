using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Configuration;
using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

public class BrowserstackDriverInitialiser
{
    private readonly Uri _browserstackRemoteServer;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISeleniumConfiguration _seleniumConfiguration;


    public BrowserstackDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ScenarioContext scenarioContext)
    {
        _seleniumConfiguration = seleniumConfiguration;
        _scenarioContext = scenarioContext;
        _browserstackRemoteServer = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
    }

    private static Lazy<string?> BrowserstackUsername =>
        new Lazy<string?>(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));

    private static Lazy<string?> AccessKey =>
        new Lazy<string?>(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));


    private string GetScenarioTitle()
    {
        var testName = _scenarioContext.ScenarioInfo.Title;

        if (_scenarioContext.ScenarioInfo.Arguments.Count > 0)
        {
            testName += ": ";
        }

        foreach (DictionaryEntry argument in _scenarioContext.ScenarioInfo.Arguments)
        {
            testName += argument.Key + ":" + argument.Value + "; ";
        }

        return testName.Trim();
    }


    public IWebDriver GetWebDriver(DriverOptions options)
    {
        return new RemoteWebDriver(_browserstackRemoteServer, options);
    }

    public T AddBrowserstackOptions<T>(T options) where T : DriverOptions
    {
        if (BrowserstackUsername.Value is not null && AccessKey.Value is not null)
        {
            TryToAddGlobalCapability(options, "browserstack.user", BrowserstackUsername.Value);
            TryToAddGlobalCapability(options, "browserstack.key", AccessKey.Value);
        }

        TryToAddGlobalCapability(options, "name", GetScenarioTitle());

        return options;
    }

    private void TryToAddGlobalCapability<T>(T options, string name, string value) where T : DriverOptions
    {
        switch (options)
        {
            case FirefoxOptions firefoxOptions:
                firefoxOptions.AddAdditionalCapability(name, value, true);
                break;
            case ChromeOptions chromeOptions:
                chromeOptions.AddAdditionalCapability(name, value, true);
                break;
            default:
                options.AddAdditionalCapability(name, value);
                break;
        }
    }
}