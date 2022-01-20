using SpecFlow.Actions.Selenium.Configuration;
using System.Linq;

namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public class LocalOptionsConfigurator : IOptionsConfigurator
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;

        public LocalOptionsConfigurator(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public void Add(IOptionsWrapper options)
        {
            if (_seleniumConfiguration.Capabilities.Any() && _seleniumConfiguration.Capabilities is not null)
            {
                foreach (var capability in _seleniumConfiguration.Capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (options.ImplementsArgs)
            {
                if (_seleniumConfiguration.Arguments is not null && _seleniumConfiguration.Arguments.Any())
                {
                    options.AddArguments(_seleniumConfiguration.Arguments);
                }
            }
            else
            {
                if (_seleniumConfiguration.Arguments is not null && _seleniumConfiguration.Arguments.Any())
                {
                    options.AddAdditionalCapability("args", _seleniumConfiguration.Arguments.ToList());
                }
            }
        }
    }
}