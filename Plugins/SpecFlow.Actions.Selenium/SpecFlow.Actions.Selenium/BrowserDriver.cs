using System;
using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Helpers;
using SpecFlow.Actions.Selenium.Factories;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly IDriverFactory _driverFactory;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        public BrowserDriver(ISeleniumConfiguration seleniumConfiguration, IDriverFactory driverFactory)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _driverFactory = driverFactory;
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// <summary>
        /// The current Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateWebDriver()
        {
            return _driverFactory.GetDriver(
                TargetHelper.GetNextTarget(
                    _seleniumConfiguration.Targets));
        }

        /// <summary>
        /// Disposes the Selenium web driver (closing the browser)
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}