using OpenQA.Selenium.Appium;

namespace SpecFlow.Actions.Appium
{
    public interface IDriverOptions
    {
        AppiumOptions Current { get; }
    }
}