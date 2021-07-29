using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        public BrowserDriver(ISeleniumConfiguration seleniumConfiguration)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// <summary>
        /// The Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateWebDriver()
        {
            switch (_seleniumConfiguration.Browser.ToLower())
            {
                case "chrome":
                    var chromeDriverService = ChromeDriverService.CreateDefaultService();

                    var chromeOptions = new ChromeOptions();

                    var chromeBinaryLocation = Environment.GetEnvironmentVariable("CHROMEWEBDRIVER");
                    if (!string.IsNullOrWhiteSpace(chromeBinaryLocation))
                    {
                        chromeOptions.BinaryLocation = chromeBinaryLocation;
                    }

                    var chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);

                    return chromeDriver;
                case "noop":
                    return new NoopWebdriver();
                    
            }

            throw new NotImplementedException($"Support for browser {_seleniumConfiguration.Browser} is not implemented yet");
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