using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    /// <summary>
    /// Manages a browser instance using Playwright
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly IPlaywrightConfiguration _playwrightConfiguration;
        private readonly IDriverInitialiser _driverInitialiser;
        protected readonly AsyncLazy<(IPlaywright playwright, IBrowser browser)> _currentBrowserLazy;
        protected bool _isDisposed;

        public BrowserDriver(IPlaywrightConfiguration playwrightConfiguration, IDriverInitialiser driverInitialiser)
        {
            _playwrightConfiguration = playwrightConfiguration;
            _driverInitialiser = driverInitialiser;
            _currentBrowserLazy = new AsyncLazy<(IPlaywright, IBrowser)>(CreatePlaywrightAsync);
        }

        /// <summary>
        /// The current Playwright instance
        /// </summary>
        public Task<(IPlaywright Playwright, IBrowser Browser)> Current => _currentBrowserLazy.Value;

        public async Task<IPlaywright> GetPlaywright()
        {
            return (await Current).Playwright;
        }

        public async Task<IBrowser> GetBrowser()
        {
            return (await Current).Browser;
        }


        /// <summary>
        /// Creates a new instance of Playwright (opens a browser)
        /// </summary>
        /// <returns></returns>
        private async Task<(IPlaywright,IBrowser)> CreatePlaywrightAsync()
        {
            return _playwrightConfiguration.Browser switch
            {
                Browser.Chrome => await _driverInitialiser.GetChromeDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
                Browser.Firefox => await _driverInitialiser.GetFirefoxDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
                Browser.Edge => await _driverInitialiser.GetEdgeDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
                Browser.Chromium => await _driverInitialiser.GetChromiumDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
                Browser.Webkit => await _driverInitialiser.GetWebKitDriverAsync(_playwrightConfiguration.Arguments, _playwrightConfiguration.DefaultTimeout, _playwrightConfiguration.Headless, _playwrightConfiguration.SlowMo, _playwrightConfiguration.TraceDir),
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
                    await (await Current).Browser.CloseAsync();
                    await (await Current).Browser.DisposeAsync();
                });
            }

            _isDisposed = true;
        }
    }
}
