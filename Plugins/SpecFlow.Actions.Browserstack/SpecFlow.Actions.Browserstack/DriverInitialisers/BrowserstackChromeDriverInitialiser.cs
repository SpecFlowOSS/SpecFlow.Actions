using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackChromeDriverInitialiser : ChromeDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackChromeDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration) : base(seleniumConfiguration)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver GetWebDriver(ChromeOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }

    protected override ChromeOptions GetChromeOptions()
    {
        var options = base.GetChromeOptions();

        return _browserstackDriverInitialiser.AddBrowserstackOptions(options);
    }
}