using OpenQA.Selenium;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium.Browsers
{
    internal class Noop : IBrowser
    {
        public IWebDriver GetDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            return new NoopWebdriver();
        }
    }
}