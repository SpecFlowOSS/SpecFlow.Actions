using Castle.Core.Internal;
using OpenQA.Selenium.Appium;
using SpecFlow.Actions.Appium.Configuration;
using System;

namespace SpecFlow.Actions.Appium
{
    internal class DriverOptions : IDriverOptions
    {
        private readonly IAppiumConfiguration _appiumConfiguration;
        private readonly Lazy<AppiumOptions> _appiumOptionsLazy;

        public AppiumOptions Current => _appiumOptionsLazy.Value;

        internal DriverOptions(IAppiumConfiguration appiumConfiguration)
        {
            _appiumConfiguration = appiumConfiguration;

            _appiumOptionsLazy = new Lazy<AppiumOptions>(GetOptions);
        }

        private AppiumOptions GetOptions()
        {
            var options = new AppiumOptions();

            if (_appiumConfiguration.Capabilities.IsNullOrEmpty())
            {
                throw new Exception("Capabilities must be provided to launch the Appium driver. please refer to the Appium documentation");
            }

            foreach (var capability in _appiumConfiguration.Capabilities!)
            {
                options.AddAdditionalCapability(capability.Key, capability.Value);
            }

            return options;
        }
    }
}