using OpenQA.Selenium;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium.Browsers
{
    public interface IBrowser
    {
        IWebDriver GetDriver(Dictionary<string, string>? capabilities = null, string[]? args = null);
    }
}