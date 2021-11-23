using BoDi;
using System;
using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly IObjectContainer _objectContainer;
        private readonly Random _random;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        public BrowserDriver(ISeleniumConfiguration seleniumConfiguration, IObjectContainer objectContainer)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _objectContainer = objectContainer;
            _random = new Random();
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

            var nextTarget = GetNextTarget(_seleniumConfiguration.Targets);

            return nextTarget.Browser switch
            {
                Browser.Chrome => initialiser.GetChromeDriver(nextTarget.Capabilities, nextTarget.Arguments),
                Browser.Firefox => initialiser.GetFirefoxDriver(nextTarget.Capabilities, nextTarget.Arguments),
                Browser.Edge => initialiser.GetEdgeDriver(nextTarget.Capabilities, nextTarget.Arguments),
                Browser.InternetExplorer => initialiser.GetInternetExplorerDriver(nextTarget.Capabilities, nextTarget.Arguments),
                Browser.Safari => initialiser.GetSafariDriver(nextTarget.Capabilities, nextTarget.Arguments),
                Browser.Noop => new NoopWebdriver(),
                _ => throw new NotImplementedException($"Support for browser {nextTarget.Browser} is not implemented yet"),
            };
        }

        // Gets the next target configuration randomly
        private ITarget GetNextTarget(List<Target> targets)
        {
            return targets[_random.Next(0, targets.Count)];
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