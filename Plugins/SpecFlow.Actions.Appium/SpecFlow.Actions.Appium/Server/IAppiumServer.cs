using OpenQA.Selenium.Appium.Service;
using System;

namespace SpecFlow.Actions.Appium.Server
{
    public interface IAppiumServer : IDisposable
    {
        AppiumLocalService Current { get; }
    }
}