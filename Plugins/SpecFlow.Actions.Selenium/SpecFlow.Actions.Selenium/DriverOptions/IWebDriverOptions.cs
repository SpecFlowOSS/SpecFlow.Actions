using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public interface IWebDriverOptions
    {
        IOptionsWrapper GetOptions(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null);
    }
}