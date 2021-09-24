using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using SpecFlow.Actions.WindowsAppDriver.Configuration;
using System;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public class AppDriver : IDisposable
    {
        private readonly IWindowsAppDriverConfiguration _windowsAppDriverConfiguration;

        private const string DriverUrl = "http://127.0.0.1:4723/";

        private readonly Lazy<WindowsDriver<WindowsElement>> _lazyDriver;
        private bool _isDisposed;

        public AppDriver(IWindowsAppDriverConfiguration windowsAppDriverConfiguration)
        {
            _windowsAppDriverConfiguration = windowsAppDriverConfiguration;
            _lazyDriver = new Lazy<WindowsDriver<WindowsElement>>(CreateAppDriver);
        }

        /// <summary>
        /// The current AppDriver instance
        /// </summary>
        public WindowsDriver<WindowsElement> Current => _lazyDriver.Value;

        /// <summary>
        /// Creates the AppDriver instance
        /// </summary>
        private WindowsDriver<WindowsElement> CreateAppDriver()
        {
            var options = new AppiumOptions();

            if (_windowsAppDriverConfiguration.Capabilities != null)
            {
                foreach (var capability in _windowsAppDriverConfiguration.Capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return new WindowsDriver<WindowsElement>(new Uri(DriverUrl), options);
        }

        /// <summary>
        /// Disposes the current AppDriver instance
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_lazyDriver.IsValueCreated)
            {
                Current.CloseApp();
            }

            _isDisposed = true;
        }
    }
}