using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SpecFlow.Actions.Selenium.build
{
    public interface IBrowserDriverInteractions
    {
        IWebElement GetElement(By elementLocator);
        void GoToUrl(string url);
        string GetUrl();
        T? WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class;
    }

    public class BrowserDriverInteractions : IBrowserDriverInteractions
    {
        private readonly BrowserDriver _browserDriver;
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly Lazy<WebDriverWait> _webDriverWait;

        public BrowserDriverInteractions(BrowserDriver browserDriver, ISeleniumConfiguration seleniumConfiguration)
        {
            _browserDriver = browserDriver;
            _seleniumConfiguration = seleniumConfiguration;
            _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
        }

        /// <summary>
        /// Gets the IWebElement by the specified element localisation
        /// </summary>
        /// <param name="elementLocator"></param>
        /// <returns></returns>
        public IWebElement GetElement(By elementLocator)
        {
            return _webDriverWait.Value.Until(_ => _browserDriver.Current.FindElement(elementLocator));
        }

        /// <summary>
        /// Goes to the specified url
        /// </summary>
        /// <param name="url"></param>
        public void GoToUrl(string url)
        {
            _browserDriver.Current.Navigate().GoToUrl(url);

            _webDriverWait.Value.Until(_ => _browserDriver.Current.Url.Equals(url));
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
                return isResultAccepted(result) ? result : default(T);
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