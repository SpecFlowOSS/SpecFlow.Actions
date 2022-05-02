using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
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
        options.TryToAddGlobalCapability("name", GetScenarioTitle());

        return new RemoteWebDriver(_browserstackRemoteServer, options);
    }
}