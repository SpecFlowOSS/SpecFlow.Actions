using OpenQA.Selenium;

namespace SpecFlow.Actions.Appium
{
    internal interface IDriverResolver
    {
        IWebDriver Resolve();
    }
}