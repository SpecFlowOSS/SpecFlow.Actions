using OpenQA.Selenium.Appium.Android;
using SpecFlow.Actions.Appium.Configuration.Appium;
using System;

namespace SpecFlow.Actions.Appium.Drivers
{
    public class AndroidAppDriver : IDisposable
    {
        private readonly IDriverFactory _driverFactory;
        private readonly IAppiumServer _appiumServer;
        private readonly IDriverOptions _driverOptions;
        private readonly IAppiumConfiguration _appiumConfiguration;
        private readonly Lazy<AndroidDriver<AndroidElement>> _lazyAndroidDriver;

        public AndroidDriver<AndroidElement> Current => _lazyAndroidDriver.Value;

        internal AndroidAppDriver(IDriverFactory driverFactory, IAppiumServer appiumServer, IDriverOptions driverOptions, IAppiumConfiguration appiumConfiguration)
        {
            _driverFactory = driverFactory;
            _appiumServer = appiumServer;
            _driverOptions = driverOptions;
            _appiumConfiguration = appiumConfiguration;
            _lazyAndroidDriver = new Lazy<AndroidDriver<AndroidElement>>(GetDriver);
        }

        private AndroidDriver<AndroidElement> GetDriver()
        {
            return _driverFactory.GetAndroidDriver(_appiumServer, _driverOptions, _appiumConfiguration);
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