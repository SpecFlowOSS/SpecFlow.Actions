using OpenQA.Selenium.Appium.Windows;
using SpecFlow.Actions.Appium.Configuration.WindowsAppDriver;
using SpecFlow.Actions.Appium.Driver;
using System;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public class AppDriver : IDisposable
    {
        private readonly IWindowsAppDriverConfiguration _windowsAppDriverConfiguration;
        private readonly IDriverFactory _driverFactory;
        private readonly IDriverOptions _driverOptions;

        private readonly Uri _driverUri = new("http://127.0.0.1:4723/");

        private readonly Lazy<WindowsDriver<WindowsElement>> _lazyDriver;
        private bool _isDisposed;

        public AppDriver(IWindowsAppDriverConfiguration windowsAppDriverConfiguration, IDriverFactory driverFactory, IDriverOptions driverOptions)
        {
            _windowsAppDriverConfiguration = windowsAppDriverConfiguration;
            _driverFactory = driverFactory;
            _driverOptions = driverOptions;
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
            return _driverFactory.GetWindowsAppDriver(_windowsAppDriverConfiguration, _driverOptions, _driverUri);
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