using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlow.Actions.Selenium
{
    public class LocalDriverOptions : IWebDriverOptions
    {
        public IOptionsWrapper GetOptions(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            IOptionsWrapper options = browser switch
            {
                Browser.Chrome => new ChromeOptionsWrapper(),
                Browser.Firefox => new FirefoxOptionsWrapper(),
                Browser.Edge => new EdgeOptionsWrapper(),
                Browser.InternetExplorer => new InternetExplorerOptionsWrapper(),
                _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null),
            };

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (options.ImplementsArgs)
            {
                if (args != null && args.Length != 0)
                {
                    options.AddArguments(args);
                }
            }
            else
            {
                if (args != null && args.Length != 0)
                {
                    options.AddAdditionalCapability("args", args.ToList());
                }
            }

            return options;
        }
    }
}