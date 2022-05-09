using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.LambdaTest.DriverInitialisers;

public class LambdaTestDriverInitialiser
{
    private readonly Uri _remoteServer;
    private readonly ScenarioContext _scenarioContext;

    public LambdaTestDriverInitialiser(LambdaTestConfiguration lambdaTestConfiguration,
        ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _remoteServer = new Uri(lambdaTestConfiguration.Url);
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

        return new RemoteWebDriver(_remoteServer, options);
    }
}