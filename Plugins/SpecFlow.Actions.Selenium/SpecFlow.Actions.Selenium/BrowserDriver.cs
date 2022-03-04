using System;
using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.DriverInitialisers;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly IDriverInitialiser _driverInitialiser;

        protected readonly Lazy<IWebDriver> CurrentWebDriverLazy;
        protected bool IsDisposed;

        public BrowserDriver(IDriverInitialiser driverInitialiser)
        {
            _driverInitialiser = driverInitialiser;
            CurrentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// <summary>
        /// The current Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => CurrentWebDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateWebDriver()
        {
            return _driverInitialiser.Initialise();
        }

        /// <summary>
        /// Disposes the Selenium web driver (closing the browser)
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            if (CurrentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            IsDisposed = true;
        }
    }
}