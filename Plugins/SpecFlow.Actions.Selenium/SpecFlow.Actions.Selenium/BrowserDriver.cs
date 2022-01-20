using BoDi;
using System;
using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Configuration;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        public BrowserDriver(IObjectContainer objectContainer, ISeleniumConfiguration seleniumConfiguration)
        {
            _objectContainer = objectContainer;
            _seleniumConfiguration = seleniumConfiguration;
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
            var initialiser = _objectContainer.Resolve<IDriverInitialiser>(_seleniumConfiguration.Browser.ToString());

            return initialiser.Initialise();
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