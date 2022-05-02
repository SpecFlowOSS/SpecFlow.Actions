using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;

namespace SpecFlow.Actions.Browserstack.DriverInitialisers;

internal class BrowserstackEdgeDriverInitialiser : EdgeDriverInitialiser
{
    private readonly BrowserstackDriverInitialiser _browserstackDriverInitialiser;

    public BrowserstackEdgeDriverInitialiser(BrowserstackDriverInitialiser browserstackDriverInitialiser,
        ISeleniumConfiguration seleniumConfiguration) : base(seleniumConfiguration)
    {
        _browserstackDriverInitialiser = browserstackDriverInitialiser;
    }

    protected override IWebDriver GetDriver(EdgeOptions options)
    {
        return _browserstackDriverInitialiser.GetWebDriver(options);
    }

    protected override EdgeOptions GetEdgeOptions()
    {
        var options = base.GetEdgeOptions();

        return _browserstackDriverInitialiser.AddBrowserstackOptions(options);
    }
}