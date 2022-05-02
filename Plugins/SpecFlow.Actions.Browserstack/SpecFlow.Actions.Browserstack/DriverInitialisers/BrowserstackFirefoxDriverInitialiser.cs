using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackFirefoxDriverInitialiser : FirefoxDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackFirefoxDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration) : base(seleniumConfiguration)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver GetWebDriver(FirefoxOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }

    protected override FirefoxOptions GetFirefoxOptions()
    {
        var options = base.GetFirefoxOptions();

        return _browserstackDriverInitialiser.AddBrowserstackOptions(options);
    }
}