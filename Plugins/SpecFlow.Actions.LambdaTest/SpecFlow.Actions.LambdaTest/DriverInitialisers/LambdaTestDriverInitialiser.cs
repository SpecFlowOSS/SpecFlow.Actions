using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using System;
using System.Collections;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlow.Actions.LambdaTest.DriverInitialisers;

public class LambdaTestDriverInitialiser
{
    private readonly Uri _remoteServer;
    private readonly ScenarioContext _scenarioContext;
    private readonly ITestAssemblyProvider _testAssemblyProvider;
    private readonly CurrentTargetIdentifier _currentTargetIdentifier;

    public LambdaTestDriverInitialiser(LambdaTestConfiguration lambdaTestConfiguration,
        ScenarioContext scenarioContext, ITestAssemblyProvider testAssemblyProvider, CurrentTargetIdentifier currentTargetIdentifier)
    {
        _scenarioContext = scenarioContext;
        _testAssemblyProvider = testAssemblyProvider;
        _currentTargetIdentifier = currentTargetIdentifier;
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

    public void AddDefaultCapabilities(DriverOptions options)
    {
        options.TryToAddGlobalCapability("projectName", _testAssemblyProvider.TestAssembly.GetName().Name);
        options.TryToAddGlobalCapability("build", _currentTargetIdentifier.Name ?? "Untitled");
        options.TryToAddGlobalCapability("name", GetScenarioTitle());
    }


    public IWebDriver GetWebDriver(DriverOptions options)
    {
        return new RemoteWebDriver(_remoteServer, options);
    }
}