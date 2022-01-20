using SpecFlow.Actions.Selenium.DriverOptions;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface IWebDriverOptionsFactory
    {
        IOptionsWrapper GetChromeOptions(Dictionary<string, string> caps, string[] args);

        IOptionsWrapper GetFireFoxOptions(Dictionary<string, string> caps, string[] args);

        IOptionsWrapper GetEdgeOptions(Dictionary<string, string> caps, string[] args);

        IOptionsWrapper GetInternetExplorerOptions(Dictionary<string, string> caps, string[] args);

        IOptionsWrapper GetSafariOptions(Dictionary<string, string> caps, string[] args);
    }
}