using SpecFlow.Actions.Selenium.DriverOptions;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlow.Actions.Selenium
{
    public class LocalOptionsConfigurator : IOptionsConfigurator
    {
        public void Add(IOptionsWrapper options, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            if (capabilities.Any() && capabilities is not null)
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
        }
    }
}