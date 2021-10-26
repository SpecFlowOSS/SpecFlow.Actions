using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using SpecFlow.Actions.Appium.Configuration;
using System;

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

        public IWebDriver Resolve(string automationName)
        {
            switch (automationName)
            {
                case "UiAutomator2":
                    return _appiumConfiguration.LocalAppiumServerRequired
                        ? new AndroidDriver<AndroidElement>(_appiumServer.Current, _driverOptions.Current, TimeSpan.FromSeconds(_appiumConfiguration.Timeout ?? 30))
                        : new AndroidDriver<AndroidElement>(_appiumConfiguration.ServerAddress, _driverOptions.Current, TimeSpan.FromSeconds(_appiumConfiguration.Timeout ?? 30));
                case "XCUITest":
                case "Espresso":
                case "Windows":
                case "Mac":
                    throw new NotImplementedException($"There is no implementation yet for {automationName}");
                default:
                    throw new ArgumentOutOfRangeException(nameof(automationName), automationName, null);
            }
        }
    }
}