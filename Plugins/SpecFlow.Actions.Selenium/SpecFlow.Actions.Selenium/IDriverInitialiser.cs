using OpenQA.Selenium;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium;

public interface IDriverInitialiser
{
    IWebDriver Initialise(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null);
}