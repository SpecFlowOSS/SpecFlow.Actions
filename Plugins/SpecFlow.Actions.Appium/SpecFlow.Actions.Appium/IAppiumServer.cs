using OpenQA.Selenium.Appium.Service;
using System;

namespace SpecFlow.Actions.Appium
{
    public interface IAppiumServer : IDisposable
    {
        AppiumLocalService Current { get; }
    }
}