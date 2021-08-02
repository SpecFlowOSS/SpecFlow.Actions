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
        private readonly IDriverInitialiser _driverInitialiser;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        public BrowserDriver(ISeleniumConfiguration seleniumConfiguration, IDriverInitialiser driverInitialiser)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _driverInitialiser = driverInitialiser;
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
            return _seleniumConfiguration.Browser switch
            {
                Browser.Chrome => _driverInitialiser.GetChromeDriver(_seleniumConfiguration.Capabilities, _seleniumConfiguration.Arguments),
                Browser.Firefox => _driverInitialiser.GetFirefoxDriver(_seleniumConfiguration.Capabilities, _seleniumConfiguration.Arguments),
                Browser.Edge => _driverInitialiser.GetEdgeDriver(_seleniumConfiguration.Capabilities),
                Browser.InternetExplorer => _driverInitialiser.GetInternetExplorerDriver(_seleniumConfiguration.Capabilities),
                Browser.Noop => new NoopWebdriver(),
                _ => throw new NotImplementedException($"Support for browser {_seleniumConfiguration.Browser} is not implemented yet"),
            };
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