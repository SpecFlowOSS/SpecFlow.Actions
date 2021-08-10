using Microsoft.Playwright;
using System;

namespace SpecFlow.Actions.Playwright
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly IPlaywrightConfiguration _playwrightConfiguration;
        private readonly IDriverInitialiser _driverInitialiser;
        protected readonly Lazy<IBrowser> _currentBrowserLazy;
        protected bool _isDisposed;

        public BrowserDriver(IPlaywrightConfiguration playwrightConfiguration, IDriverInitialiser driverInitialiser)
        {
            _playwrightConfiguration = playwrightConfiguration;
            _driverInitialiser = driverInitialiser;
            _currentBrowserLazy = new Lazy<IBrowser>(CreateWebDriver);
        }

        /// <summary>
        /// The current Selenium IWebDriver instance
        /// </summary>
        public IBrowser Current => _currentBrowserLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IBrowser CreateWebDriver()
        {
            return _playwrightConfiguration.Browser switch
            {
                Browser.Chrome => _driverInitialiser.GetChromeDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments).Result,
                Browser.Firefox => _driverInitialiser.GetFirefoxDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments).Result,
                Browser.Edge => _driverInitialiser.GetEdgeDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments).Result,
                Browser.Chromium => _driverInitialiser.GetChromiumDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments).Result,
                Browser.Noop => new NoopWebdriver(),
                _ => throw new NotImplementedException($"Support for browser {_playwrightConfiguration.Browser} is not implemented yet"),
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

            if (_currentBrowserLazy.IsValueCreated)
            {
                Current.CloseAsync();
            }

            _isDisposed = true;
        }
    }
}