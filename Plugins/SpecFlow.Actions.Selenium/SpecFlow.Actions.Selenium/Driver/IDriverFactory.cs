using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium.Driver;

public interface IDriverFactory
{
    IWebDriver GetEdgeDriver();
    IWebDriver GetInternetExplorerDriver();
    IWebDriver GetFirefoxDriver();
    IWebDriver GetChromeDriver();
    IWebDriver GetSafariDriver();
}