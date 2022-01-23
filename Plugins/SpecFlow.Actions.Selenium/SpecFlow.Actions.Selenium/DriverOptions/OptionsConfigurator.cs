using SpecFlow.Actions.Selenium.Configuration;
using System.Linq;

namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public class OptionsConfigurator : IOptionsConfigurator
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;

        public OptionsConfigurator(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
        }

        public void Add(IDriverOptions options)
        {
            if (_seleniumConfiguration.Capabilities.Any())
            {
                foreach (var capability in _seleniumConfiguration.Capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (options.ImplementsArgs)
            {
                if (_seleniumConfiguration.Arguments.Any())
                {
                    options.AddArguments(_seleniumConfiguration.Arguments);
                }
            }
            else
            {
                if (_seleniumConfiguration.Arguments.Any())
                {
                    options.AddAdditionalCapability("args", _seleniumConfiguration.Arguments.ToList());
                }
            }
        }
    }
}