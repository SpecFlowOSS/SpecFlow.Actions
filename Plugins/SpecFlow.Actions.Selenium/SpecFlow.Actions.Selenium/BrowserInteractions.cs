using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface IBrowserInteractions
    {
        IWebElement WaitAndReturnElement(By elementLocator);
        IEnumerable<IWebElement> WaitAndReturnElements(By elementLocator);
        void GoToUrl(string url);
        string GetUrl();
        T? WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class;
    }

    public class BrowserInteractions : IBrowserInteractions
    {
        private readonly BrowserDriver _browserDriver;
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly Lazy<WebDriverWait> _webDriverWait;
        
        public BrowserInteractions(BrowserDriver browserDriver, ISeleniumConfiguration seleniumConfiguration)
        {
            _browserDriver = browserDriver;
            _seleniumConfiguration = seleniumConfiguration;
            _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
        }

        /// <summary>
        /// Waits for the element to exist and returns it using the specified element localisation
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <returns></returns>
        public IWebElement WaitAndReturnElement(By elementLocator)
        {
            return _webDriverWait.Value.Until(_ => _browserDriver.Current.FindElement(elementLocator));
        }

        /// <summary>
        /// Waits for the element(s) to exist and returns them using the specified element localisation
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <returns></returns>
        public IEnumerable<IWebElement> WaitAndReturnElements(By elementLocator)
        {
            return _webDriverWait.Value.Until(_ => _browserDriver.Current.FindElements(elementLocator));
        }

        /// <summary>
        /// Goes to the specified url
        /// </summary>
        /// <param name="url"></param>
        public void GoToUrl(string url)
        {
            _browserDriver.Current.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Gets the current URL
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return _browserDriver.Current.Url;
        }

        /// <summary>
        /// Helper method to wait until the expected result is available on the UI
        /// </summary>
        /// <typeparam name="T">The type of result to retrieve</typeparam>
        /// <param name="getResult">The function to poll the result from the UI</param>
        /// <param name="isResultAccepted">The function to decide if the polled result is accepted</param>
        /// <returns>An accepted result returned from the UI. If the UI does not return an accepted result within the timeout an exception is thrown.</returns>
        public T? WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class
        {
            T? Condition(IWebDriver driver)
            {
                var result = getResult();
                return isResultAccepted(result) ? result : default;
            }

            return _webDriverWait.Value.Until(Condition);
        }

        private WebDriverWait GetWaitDriver()
        {
            return new(_browserDriver.Current, timeout: TimeSpan.FromSeconds(_seleniumConfiguration.DefaultTimeout ?? 30))
            {
                PollingInterval = TimeSpan.FromSeconds(_seleniumConfiguration.PollingInterval ?? 1)
            };
        }
    }
}