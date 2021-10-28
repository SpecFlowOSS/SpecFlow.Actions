using OpenQA.Selenium.Appium;

namespace SpecFlow.Actions.Appium.Driver
{
    public interface IDriverOptions
    {
        AppiumOptions Current { get; }
    }
}