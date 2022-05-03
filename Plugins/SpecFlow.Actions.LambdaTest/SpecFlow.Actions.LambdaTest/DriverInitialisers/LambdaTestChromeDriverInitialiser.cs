using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using SpecFlow.Actions.Selenium.Hoster;
using System;

namespace SpecFlow.Actions.LambdaTest.DriverInitialisers;

internal class LambdaTestChromeDriverInitialiser : ChromeDriverInitialiser
{
    private readonly LambdaTestDriverInitialiser _lambdaTestDriverInitialiser;

    public LambdaTestChromeDriverInitialiser(LambdaTestDriverInitialiser lambdaTestDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(
        seleniumConfiguration, credentialProvider)
    {
        _lambdaTestDriverInitialiser = lambdaTestDriverInitialiser;
    }


    protected override IWebDriver CreateWebDriver(ChromeOptions options)
    {
        return _lambdaTestDriverInitialiser.GetWebDriver(options);
    }
}