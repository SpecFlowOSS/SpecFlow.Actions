using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlow.Actions.Selenium
{
    internal class LocalDriverOptions : IDriverOptions
    {
        public ChromeOptions Chrome(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new ChromeOptions();

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (args != null && args.Length != 0)
            {
                options.AddArguments(args);
            }

            return options;
        }

        public EdgeOptions Edge(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new EdgeOptions();

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (args != null && args.Length != 0)
            {
                options.AddAdditionalCapability("args", args.ToList());
            }

            return options;
        }

        public FirefoxOptions Firefox(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new FirefoxOptions();

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (args != null && args.Length != 0)
            {
                options.AddArguments(args);
            }

            return options;
        }

        public InternetExplorerOptions InternetExplorer(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new InternetExplorerOptions();

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (args != null && args.Length != 0)
            {
                options.AddAdditionalCapability("args", args.ToList());
            }

            return options;
        }

        public SafariOptions Safari(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            // Can't be tested without a mac.
            throw new NotImplementedException();
        }
    }
}