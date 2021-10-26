using OpenQA.Selenium;
using SpecFlow.Actions.Appium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Appium
{
    public class AppDriver : IAppDriver
    {
        private readonly IAppiumConfiguration _appiumConfiguration;
        private readonly IDriverResolver _driverResolver;
        private readonly Lazy<IWebDriver> _lazyAndroidDriver;

        public IWebDriver Current => _lazyAndroidDriver.Value;

        internal AppDriver(IDriverResolver driverResolver, IAppiumConfiguration appiumConfiguration)
        {
            _driverResolver = driverResolver;
            _appiumConfiguration = appiumConfiguration;

            _lazyAndroidDriver = new Lazy<IWebDriver>(GetDriver);
        }

        private IWebDriver GetDriver()
        {
            return _driverResolver.Resolve(_appiumConfiguration.Capabilities!.Single(cap => cap.Key.Equals("automationName")).Value);
        }

        public void Dispose()
        {
            if (_lazyAndroidDriver.IsValueCreated)
            {
                Current.Dispose();
            }
        }
    }
}