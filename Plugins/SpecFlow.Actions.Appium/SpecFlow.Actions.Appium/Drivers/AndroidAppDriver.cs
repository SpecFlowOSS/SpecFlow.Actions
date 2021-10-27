using OpenQA.Selenium.Appium.Android;
using System;

namespace SpecFlow.Actions.Appium.Drivers
{
    public class AndroidAppDriver : IDisposable
    {
        private readonly IDriverFactory _driverFactory;
        private readonly Lazy<AndroidDriver<AndroidElement>> _lazyAndroidDriver;

        public AndroidDriver<AndroidElement> Current => _lazyAndroidDriver.Value;

        internal AndroidAppDriver(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
            _lazyAndroidDriver = new Lazy<AndroidDriver<AndroidElement>>(GetDriver);
        }

        private AndroidDriver<AndroidElement> GetDriver()
        {
            return _driverFactory.GetAndroidDriver();
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