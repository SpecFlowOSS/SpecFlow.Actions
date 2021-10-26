using OpenQA.Selenium;
using System;

namespace SpecFlow.Actions.Appium
{
    public interface IAppDriver : IDisposable
    {
        IWebDriver Current { get; }
    }
}