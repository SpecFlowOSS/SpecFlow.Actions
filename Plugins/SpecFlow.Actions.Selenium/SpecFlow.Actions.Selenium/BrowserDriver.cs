using System;
using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Helpers;
using SpecFlow.Actions.Selenium.Factories;
using BoDi;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly IObjectContainer _objectContainer;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        public BrowserDriver(ISeleniumConfiguration seleniumConfiguration, IObjectContainer objectContainer)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _objectContainer = objectContainer;
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
            var driverFactory = _objectContainer.Resolve<IDriverFactory>(_seleniumConfiguration.TestPlatform);

            return driverFactory.GetDriver(
                BrowserConfigurationHelper.GetRandomConfiguration(
                    _seleniumConfiguration.BrowserConfigurations));
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