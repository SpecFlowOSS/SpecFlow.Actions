using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using SpecFlow.Actions.Selenium.Hoster;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackSafariDriverInitialiser : SafariDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackSafariDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) : base(seleniumConfiguration, credentialProvider)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver CreateWebDriver(SafariOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }
}