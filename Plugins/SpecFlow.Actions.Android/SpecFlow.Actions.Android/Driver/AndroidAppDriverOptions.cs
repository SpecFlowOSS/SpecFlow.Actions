using OpenQA.Selenium.Appium;
using SpecFlow.Actions.Appium.Configuration.Android;
using SpecFlow.Actions.Appium.Driver;
using System;

namespace SpecFlow.Actions.Android.Driver
{
    public class AndroidAppDriverOptions : IDriverOptions
    {
        private readonly IAndroidConfiguration _androidConfiguration;
        private readonly Lazy<AppiumOptions> _appiumOptionsLazy;

        public AppiumOptions Current => _appiumOptionsLazy.Value;

        public AndroidAppDriverOptions(IAndroidConfiguration androidConfiguration)
        {
            _androidConfiguration = androidConfiguration;
            _appiumOptionsLazy = new Lazy<AppiumOptions>(GetOptions);
        }

        private AppiumOptions GetOptions()
        {
            var options = new AppiumOptions();

            foreach (var capability in _androidConfiguration.Capabilities)
            {
                options.AddAdditionalCapability(capability.Key, capability.Value);
            }

            return options;
        }
    }
}