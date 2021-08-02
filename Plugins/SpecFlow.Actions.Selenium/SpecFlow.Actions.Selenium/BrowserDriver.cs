using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlow.Actions.Selenium
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        protected readonly Lazy<IWebDriver> _currentWebDriverLazy;
        protected bool _isDisposed;

        // TODO: We don't need to get this every time
        private string ChromeFilePath => Environment.GetEnvironmentVariable("CHROMEWEBDRIVER");

        public BrowserDriver(ISeleniumConfiguration seleniumConfiguration, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _specFlowOutputHelper = specFlowOutputHelper;
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
            return _seleniumConfiguration.Browser switch
            {
                Browser.Chrome => GetChromeDriver(ChromeFilePath),
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

        private IWebDriver GetChromeDriver(string filepath) 
        {
            var chromeOptions = new ChromeOptions();

            if (_seleniumConfiguration.Arguments?.Length != 0)
            {
                chromeOptions.AddArguments(_seleniumConfiguration.Arguments);
            }

            return new ChromeDriver(string.IsNullOrWhiteSpace(filepath) ?
                ChromeDriverService.CreateDefaultService() : ChromeDriverService.CreateDefaultService(filepath), chromeOptions);
        }
    }
}