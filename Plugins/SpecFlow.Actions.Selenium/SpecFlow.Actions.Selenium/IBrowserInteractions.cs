using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface IBrowserInteractions
    {
        IWebElement WaitAndReturnElement(By elementLocator);
        IEnumerable<IWebElement> WaitAndReturnElements(By elementLocator);
        void GoToUrl(string url);
        string GetUrl();
        T? WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class;
    }
}