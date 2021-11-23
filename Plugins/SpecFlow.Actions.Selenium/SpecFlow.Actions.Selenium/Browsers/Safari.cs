using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium.Browsers
{
    internal class Safari : IBrowser
    {
        public IWebDriver GetDriver(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            // Can't be tested without a mac.
            throw new NotImplementedException();
        }
    }
}