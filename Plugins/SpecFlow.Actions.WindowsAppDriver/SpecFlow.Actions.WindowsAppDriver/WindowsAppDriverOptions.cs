using OpenQA.Selenium.Appium;
using SpecFlow.Actions.Appium;
using SpecFlow.Actions.Appium.Configuration.WindowsAppDriver;
using SpecFlow.Actions.Appium.Driver;
using System;
using System.Collections;
using System.IO;

namespace SpecFlow.Actions.WindowsAppDriver
{
    internal class WindowsAppDriverOptions : IDriverOptions
    {
        private readonly IWindowsAppDriverConfiguration _windowsAppDriverConfiguration;
        private readonly Lazy<AppiumOptions> _appiumOptionsLazy;

        public AppiumOptions Current => _appiumOptionsLazy.Value;

        internal WindowsAppDriverOptions(IWindowsAppDriverConfiguration windowsAppDriverConfiguration)
        {
            _windowsAppDriverConfiguration = windowsAppDriverConfiguration;
            _appiumOptionsLazy = new Lazy<AppiumOptions>(GetOptions);
        }

        private AppiumOptions GetOptions()
        {
            var options = new AppiumOptions();

            var appFilePath = Environment.GetEnvironmentVariable("TEST_SUBJECT_FILE_PATH");

            if (appFilePath != null)
            {
                options.AddAdditionalCapability("app", appFilePath);
            }

            foreach (var capability in _windowsAppDriverConfiguration.Capabilities)
            {
                if (string.Equals(capability.Key, "app", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(capability.Key, "appWorkingDir", StringComparison.OrdinalIgnoreCase))
                {
                    options.AddAdditionalCapability(capability.Key, Path.Combine(Directory.GetCurrentDirectory(), capability.Value));
                }
                else
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return options;
        }
    }
}