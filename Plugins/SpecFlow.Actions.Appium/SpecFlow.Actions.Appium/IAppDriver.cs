using OpenQA.Selenium.Appium.Android;
using System;

namespace SpecFlow.Actions.Appium
{
    public interface IAppDriver : IDisposable
    {
        AndroidDriver<AndroidElement> Current { get; }
    }
}