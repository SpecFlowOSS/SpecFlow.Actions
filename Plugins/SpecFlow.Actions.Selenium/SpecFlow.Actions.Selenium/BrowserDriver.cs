using BoDi;
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
            var initialiser = _objectContainer.Resolve<IDriverInitialiser>(_seleniumConfiguration.TestPlatform);

            return _seleniumConfiguration.Browser switch
            {
                Browser.Chrome => initialiser.GetChromeDriver(_seleniumConfiguration.Capabilities, _seleniumConfiguration.Arguments),
                Browser.Firefox => initialiser.GetFirefoxDriver(_seleniumConfiguration.Capabilities, _seleniumConfiguration.Arguments),
                Browser.Edge => initialiser.GetEdgeDriver(_seleniumConfiguration.Capabilities, _seleniumConfiguration.Arguments),
                Browser.InternetExplorer => initialiser.GetInternetExplorerDriver(_seleniumConfiguration.Capabilities, _seleniumConfiguration.Arguments),
                Browser.Safari => initialiser.GetSafariDriver(_seleniumConfiguration.Capabilities, _seleniumConfiguration.Arguments),
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