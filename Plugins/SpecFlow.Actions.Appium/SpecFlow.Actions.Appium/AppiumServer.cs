using OpenQA.Selenium.Appium.Service;
using System;

namespace SpecFlow.Actions.Appium
{
    internal class AppiumServer : IAppiumServer
    {
        private readonly Lazy<AppiumLocalService> _appiumLocalServiceLazy = new Lazy<AppiumLocalService>(new AppiumServiceBuilder().UsingAnyFreePort().Build);

        public AppiumLocalService Current => _appiumLocalServiceLazy.Value;

        public void Dispose()
        {
            if (Current.IsRunning)
            {
                Current.Dispose();
            }
        }
    }
}