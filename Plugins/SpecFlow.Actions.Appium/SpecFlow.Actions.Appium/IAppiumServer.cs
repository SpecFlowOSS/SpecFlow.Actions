using OpenQA.Selenium.Appium.Service;
using System;

namespace SpecFlow.Actions.Appium
{
    internal interface IAppiumServer : IDisposable
    {
        AppiumLocalService Current { get; }
    }
}