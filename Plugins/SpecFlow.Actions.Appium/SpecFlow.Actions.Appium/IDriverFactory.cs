using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using SpecFlow.Actions.Appium.Configuration.Appium;
using SpecFlow.Actions.Appium.Configuration.WindowsAppDriver;
using System;

namespace SpecFlow.Actions.Appium
{
    public interface IDriverFactory
    {
        AndroidDriver<AndroidElement> GetAndroidDriver(IAppiumServer appiumServer, IDriverOptions driverOptions, IAppiumConfiguration appiumConfiguration);

        void GetXCUITestDriver();

        void GetEspressoDriver();

        WindowsDriver<WindowsElement> GetWindowsAppDriver(IWindowsAppDriverConfiguration windowsAppDriverConfiguration, IDriverOptions driverOptions, Uri driverUrl);

        void GetMacDriver();
    }
}