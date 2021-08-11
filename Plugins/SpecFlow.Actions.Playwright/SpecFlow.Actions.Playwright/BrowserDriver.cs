using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly IPlaywrightConfiguration _playwrightConfiguration;
        private readonly IDriverInitialiser _driverInitialiser;
        protected readonly AsyncLazy<IBrowser> _currentBrowserLazy;
        protected bool _isDisposed;

        public BrowserDriver(IPlaywrightConfiguration playwrightConfiguration, IDriverInitialiser driverInitialiser)
        {
            _playwrightConfiguration = playwrightConfiguration;
            _driverInitialiser = driverInitialiser;
            _currentBrowserLazy = new AsyncLazy<IBrowser>(CreateWebDriverAsync);
        }

        /// <summary>
        /// The current Selenium IWebDriver instance
        /// </summary>
        public Task<IBrowser> Current => _currentBrowserLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private async Task<IBrowser> CreateWebDriverAsync()
        {
            return _playwrightConfiguration.Browser switch
            {
                Browser.Chrome => await _driverInitialiser.GetChromeDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments),
                Browser.Firefox => await _driverInitialiser.GetFirefoxDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments),
                Browser.Edge => await _driverInitialiser.GetEdgeDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments),
                Browser.Chromium => await _driverInitialiser.GetChromiumDriverAsync(_playwrightConfiguration.Capabilities, _playwrightConfiguration.Arguments),
                //Browser.Noop => new NoopWebdriver(),
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
                Task.Run(async delegate
                {
                    await (await Current).CloseAsync();
                    await (await Current).DisposeAsync();
                });
            }

            _isDisposed = true;
        }
    }
}