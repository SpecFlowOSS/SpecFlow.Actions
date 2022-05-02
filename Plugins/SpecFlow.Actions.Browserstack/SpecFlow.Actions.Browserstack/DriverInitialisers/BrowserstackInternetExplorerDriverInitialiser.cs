using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using SpecFlow.Actions.Selenium.Hoster;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackInternetExplorerDriverInitialiser : InternetExplorerDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackInternetExplorerDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(InternetExplorerOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }
}