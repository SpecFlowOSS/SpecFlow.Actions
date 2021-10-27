using OpenQA.Selenium.Appium.Android;
using SpecFlow.Actions.Appium.Configuration;
using System;

namespace SpecFlow.Actions.Appium
{
    internal class DriverFactory : IDriverFactory
    {
        private readonly IAppiumServer _appiumServer;
        private readonly IDriverOptions _driverOptions;
        private readonly IAppiumConfiguration _appiumConfiguration;

        internal DriverFactory(IAppiumServer appiumServer, IDriverOptions driverOptions, IAppiumConfiguration appiumConfiguration)
        {
            _appiumServer = appiumServer;
            _driverOptions = driverOptions;
            _appiumConfiguration = appiumConfiguration;
        }

        public AndroidDriver<AndroidElement> GetAndroidDriver()
        {
            return _appiumConfiguration.LocalAppiumServerRequired

                ? new AndroidDriver<AndroidElement>(_appiumServer.Current, _driverOptions.Current,
                    TimeSpan.FromSeconds(_appiumConfiguration.Timeout ?? 30))

                : new AndroidDriver<AndroidElement>(_appiumConfiguration.ServerAddress, _driverOptions.Current,
                    TimeSpan.FromSeconds(_appiumConfiguration.Timeout ?? 30));
        }

        public void GetXCUITestDriver()
        {
            throw new NotImplementedException("XCUITestDriver not yet implemented");
        }

        public void GetEspressoDriver()
        {
            throw new NotImplementedException("EspressoDriver not yet implemented");
        }

        public void GetWindowsAppDriver()
        {
            throw new NotImplementedException("WindowsAppDriver not yet implemented");
        }

        public void GetMacDriver()
        {
            throw new NotImplementedException("MacDriver not yet implemented");
        }
    }
}