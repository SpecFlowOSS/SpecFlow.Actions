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
            _currentBrowserLazy = new AsyncLazy<IBrowser>(CreatePlaywrightAsync);
        }

        /// <summary>
        /// The current Playwright instance
        /// </summary>
        public Task<IBrowser> Current => _currentBrowserLazy.Value;

        /// <summary>
        /// Creates a new instance of Playwright (opens a browser)
        /// </summary>
        /// <returns></returns>
        private async Task<IBrowser> CreatePlaywrightAsync()
        {
            return _playwrightConfiguration.Browser switch
            {
                Browser.Chrome => await _driverInitialiser.GetChromeDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo),
                Browser.Firefox => await _driverInitialiser.GetFirefoxDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo),
                Browser.Edge => await _driverInitialiser.GetEdgeDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo),
                Browser.Chromium => await _driverInitialiser.GetChromiumDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo),
                Browser.Webkit => await _driverInitialiser.GetWebKitDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo),
                _ => throw new NotImplementedException($"Support for browser {_playwrightConfiguration.Browser} is not implemented yet"),
            };
        }

        /// <summary>
        /// Disposes the Playwright instance (closing the browser)
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