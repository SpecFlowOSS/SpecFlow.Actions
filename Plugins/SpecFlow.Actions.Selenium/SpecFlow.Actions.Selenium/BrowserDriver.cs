using System;
using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly IDriverInitialiserResolver _driverInitialiserResolver;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        public BrowserDriver(ISeleniumConfiguration seleniumConfiguration, IDriverInitialiserResolver driverInitialiserResolver)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _driverInitialiserResolver = driverInitialiserResolver;
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
            var resolvedType = _driverInitialiserResolver.GetInitialiser(_seleniumConfiguration.Browser);

            return resolvedType.GetDriverInstance(_seleniumConfiguration.Capabilities,
                _seleniumConfiguration.Arguments);
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