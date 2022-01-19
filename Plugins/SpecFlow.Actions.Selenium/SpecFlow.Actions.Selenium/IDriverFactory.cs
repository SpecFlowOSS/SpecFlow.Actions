using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.DriverOptions;

namespace SpecFlow.Actions.Selenium;

public interface IDriverFactory
{
    IWebDriver GetEdgeDriver(IOptionsWrapper options);
    IWebDriver GetInternetExplorerDriver(IOptionsWrapper options);
    IWebDriver GetFirefoxDriver(IOptionsWrapper options);
    IWebDriver GetChromeDriver(IOptionsWrapper options);
}