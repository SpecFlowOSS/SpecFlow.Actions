using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface IDriverOptions
    {
        public EdgeOptions Edge(Dictionary<string, string>? capabilities = null, string[]? args = null);

        public ChromeOptions Chrome(Dictionary<string, string>? capabilities = null, string[]? args = null);

        public FirefoxOptions Firefox(Dictionary<string, string>? capabilities = null, string[]? args = null);

        public InternetExplorerOptions InternetExplorer(Dictionary<string, string>? capabilities = null, string[]? args = null);

        public SafariOptions Safari(Dictionary<string, string>? capabilities = null, string[]? args = null);
    }
}