using OpenQA.Selenium.Appium;

namespace SpecFlow.Actions.Appium
{
    internal interface IDriverOptions
    {
        AppiumOptions Current { get; }
    }
}