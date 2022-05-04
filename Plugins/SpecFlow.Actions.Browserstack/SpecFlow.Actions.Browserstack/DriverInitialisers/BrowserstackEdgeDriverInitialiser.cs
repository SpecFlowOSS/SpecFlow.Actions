using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using SpecFlow.Actions.Selenium.Hoster;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackEdgeDriverInitialiser : EdgeDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackEdgeDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(EdgeOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }
}