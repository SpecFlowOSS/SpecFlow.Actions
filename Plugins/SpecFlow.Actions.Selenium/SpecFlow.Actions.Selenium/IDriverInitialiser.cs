using OpenQA.Selenium;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface IDriverInitialiser
    {
        IWebDriver GetDriverInstance(Dictionary<string, string>? capabilities = null, string[]? args = null);
    }
}