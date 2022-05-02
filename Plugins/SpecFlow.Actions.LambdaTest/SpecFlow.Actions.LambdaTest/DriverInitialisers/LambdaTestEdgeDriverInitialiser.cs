using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using SpecFlow.Actions.Selenium.Hoster;
using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.LambdaTest.DriverInitialisers;

internal class LambdaTestEdgeDriverInitialiser : EdgeDriverInitialiser
{
    private readonly Uri _remoteServer;
    private readonly ScenarioContext _scenarioContext;

    public LambdaTestEdgeDriverInitialiser(ISeleniumConfiguration seleniumConfiguration,
        ScenarioContext scenarioContext, ICredentialProvider credentialProvider) : base(seleniumConfiguration,
        credentialProvider)
    {
        _scenarioContext = scenarioContext;
        _remoteServer = new Uri("http://" + credentialProvider.Username + ":" + credentialProvider.AccessKey +
                                "@hub.lambdatest.com" + "/wd/hub/");
    }


    protected override IWebDriver CreateWebDriver(EdgeOptions options)
    {
        options.AddAdditionalCapability("name", GetScenarioTitle());
        return new RemoteWebDriver(_remoteServer, options);
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
}