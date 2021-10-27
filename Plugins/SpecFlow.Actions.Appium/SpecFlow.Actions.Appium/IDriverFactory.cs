using OpenQA.Selenium.Appium.Android;

namespace SpecFlow.Actions.Appium
{
    public interface IDriverFactory
    {
        AndroidDriver<AndroidElement> GetAndroidDriver();
        void GetXCUITestDriver();
        void GetEspressoDriver();
        void GetWindowsAppDriver();
        void GetMacDriver();
    }
}