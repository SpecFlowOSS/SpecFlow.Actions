using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using SpecFlow.Actions.Appium.Configuration;
using System;
using System.Linq;

namespace SpecFlow.Actions.Appium
{
    internal class DriverResolver : IDriverResolver
    {
        private readonly IAppiumServer _appiumServer;
        private readonly IDriverOptions _driverOptions;
        private readonly IAppiumConfiguration _appiumConfiguration;

        public DriverResolver(IAppiumServer appiumServer, IDriverOptions driverOptions, IAppiumConfiguration appiumConfiguration)
        {
            _appiumServer = appiumServer;
            _driverOptions = driverOptions;
            _appiumConfiguration = appiumConfiguration;
        }

        // See https://github.com/appium/appium/blob/master/docs/en/drivers/mac.md some driver user automation name and some use platform name...
        public IWebDriver Resolve()
        {
            var automationName = _appiumConfiguration.Capabilities!.Single(cap => cap.Key.Equals("automationName")).Value;

            return _appiumConfiguration.Capabilities!.Single(cap => cap.Key.Equals("automationName")).Value switch
            {
                "UiAutomator2" => _appiumConfiguration.LocalAppiumServerRequired
                                       ? new AndroidDriver<AndroidElement>(_appiumServer.Current, _driverOptions.Current, TimeSpan.FromSeconds(_appiumConfiguration.Timeout ?? 30))
                                       : new AndroidDriver<AndroidElement>(_appiumConfiguration.ServerAddress, _driverOptions.Current, TimeSpan.FromSeconds(_appiumConfiguration.Timeout ?? 30)),
                "XCUITest" or "Espresso" or "Windows" or "Mac" => throw new NotImplementedException($"There is no implementation yet for {automationName}"),
                _ => throw new ArgumentOutOfRangeException(nameof(automationName), automationName, null),
            };
        }
    }
}