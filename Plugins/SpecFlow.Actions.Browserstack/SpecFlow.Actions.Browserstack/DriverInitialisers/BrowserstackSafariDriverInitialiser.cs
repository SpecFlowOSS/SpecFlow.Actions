using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackSafariDriverInitialiser : SafariDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackSafariDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration) : base(seleniumConfiguration)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver GetWebDriver(SafariOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }

    protected override SafariOptions GetSafariOptions()
    {
        var options = base.GetSafariOptions();

        return _browserstackDriverInitialiser.AddBrowserstackOptions(options);
    }
}