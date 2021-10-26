using OpenQA.Selenium;
using System;

namespace SpecFlow.Actions.Appium
{
    public class AppDriver : IAppDriver
    {
        private readonly IDriverResolver _driverResolver;
        private readonly Lazy<IWebDriver> _lazyAndroidDriver;

        public IWebDriver Current => _lazyAndroidDriver.Value;

        internal AppDriver(IDriverResolver driverResolver)
        {
            _driverResolver = driverResolver;
            _lazyAndroidDriver = new Lazy<IWebDriver>(GetDriver);
        }

        private IWebDriver GetDriver()
        {
            return _driverResolver.Resolve();
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