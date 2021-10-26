using OpenQA.Selenium.Appium.Android;
using SpecFlow.Actions.Appium.Configuration;
using System;

namespace SpecFlow.Actions.Appium
{
    public class AppDriver : IAppDriver
    {
        private readonly IAppiumServer _appiumServer;
        private readonly IDriverOptions _driverOptions;
        private readonly IAppiumConfiguration _appiumConfiguration;
        private readonly Lazy<AndroidDriver<AndroidElement>> _lazyAndroidDriver;

        public AndroidDriver<AndroidElement> Current => _lazyAndroidDriver.Value;

        internal AppDriver(IAppiumServer appiumServer, IDriverOptions driverOptions, IAppiumConfiguration appiumConfiguration)
        {
            _appiumServer = appiumServer;
            _driverOptions = driverOptions;
            _appiumConfiguration = appiumConfiguration;

            _lazyAndroidDriver = new Lazy<AndroidDriver<AndroidElement>>(GetDriver);
        }

        private AndroidDriver<AndroidElement> GetDriver()
        {
            return _appiumConfiguration.LocalAppiumServerRequired 
                ? new AndroidDriver<AndroidElement>(_appiumServer.Current, _driverOptions.Current) 
                : new AndroidDriver<AndroidElement>(_appiumConfiguration.ServerAddress, _driverOptions.Current);
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