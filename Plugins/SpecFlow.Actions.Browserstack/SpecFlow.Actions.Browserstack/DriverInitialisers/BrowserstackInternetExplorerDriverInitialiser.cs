using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackInternetExplorerDriverInitialiser : InternetExplorerDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackInternetExplorerDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration) : base(seleniumConfiguration)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver GetWebDriver(InternetExplorerOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }

    protected override InternetExplorerOptions GetInternetExplorerOptions()
    {
        var options = base.GetInternetExplorerOptions();

        return _browserstackDriverInitialiser.AddBrowserstackOptions(options);
    }
}