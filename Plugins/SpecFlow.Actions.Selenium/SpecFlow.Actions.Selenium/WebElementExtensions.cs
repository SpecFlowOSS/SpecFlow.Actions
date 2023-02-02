using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlow.Actions.Selenium
{
    public static class WebElementExtensions
    {
        /// <summary>
        /// Clears and sends keystrokes to the specified web element
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="keys"></param>
        public static void SendKeysWithClear(this IWebElement webElement, string keys)
        {
            webElement.Clear();
            webElement.SendKeys(keys);
        }

        /// <summary>
        /// Attempts to click the specified web element, ignoring intercepted clicks for a number of attempts
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="retryCount"></param>
        public static void ClickWithRetry(this IWebElement webElement, int retryCount = 3)
        {
            const int count = 0;
            Exception? exception = null;

            while (count < retryCount)
            {
                try
                {
                    webElement.Click();

                    return;
                }
                catch (ElementClickInterceptedException e)
                {
                    exception = e;
                    Console.WriteLine($"Note: The web element \"{webElement}\" click attempt was intercepted");
                    retryCount++;
                }
            }

            throw new Exception($"Unable to click {webElement} after {retryCount} attempt(s)", exception);
        }

        /// <summary>
        /// Returns a new select element
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static SelectElement GetSelectElement(this IWebElement webElement)
        {
            return new SelectElement(webElement);
        }

        /// <summary>
        /// Selects an option from a select element by index value
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="index"></param>
        public static void SelectDropdownOptionByIndex(this IWebElement webElement, int index)
        {
            GetSelectElement(webElement).SelectByIndex(index);
        }

        /// <summary>
        /// Selects an option from a select element by text
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="text"></param>
        public static void SelectDropdownOptionByText(this IWebElement webElement, string text)
        {
            GetSelectElement(webElement).SelectByText(text);
        }

        /// <summary>
        /// Selects an option from a select element by its value
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="value"></param>
        public static void SelectDropdownOptionByValue(this IWebElement webElement, string value)
        {
            GetSelectElement(webElement).SelectByText(value);
        }

        /// <summary>
        /// Selects a random dropdown option
        /// </summary>
        /// <param name="webElement"></param>
        public static void SelectRandomDropdownOption(this IWebElement webElement)
        {
            var selectElement = GetSelectElement(webElement);
            var random = new Random().Next(selectElement.Options.Count - 1);
            selectElement.SelectByIndex(random);
        }

        /// <summary>
        /// Finds and returns web elements that contain the specified class name
        /// </summary>
        /// <param name="webElements"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IEnumerable<IWebElement> WhereElementsHaveClass(this IEnumerable<IWebElement> webElements, string className)
        {
            return webElements.Where(element => element.GetAttribute("class").Contains(className));
        }

        /// <summary>
        /// Finds and returns web elements that have the specified value
        /// </summary>
        /// <param name="webElements"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<IWebElement> WhereElementsHaveValue(this IEnumerable<IWebElement> webElements, string value)
        {
            return webElements.Where(element => element.GetAttribute("value").Equals(value));
        }

        /// <summary>
        /// Checks if the web element contains a specified class name
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static bool HasClass(this IWebElement webElement, string className)
        {
            return webElement.GetAttribute("class").Contains(className);
        }

        /// <summary>
        /// Checks if the web element contains a specified value from its value attribute
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasValue(this IWebElement webElement, string value)
        {
            return webElement.GetAttribute("value").Equals(value);
        }

        /// <summary>
        /// Returns only web elements that are displayed
        /// </summary>
        /// <param name="webElements"></param>
        /// <returns></returns>
        public static IEnumerable<IWebElement> WhereElementsAreDisplayed(this IEnumerable<IWebElement> webElements)
        {
            return webElements.Where(element => element.Displayed.Equals(true));
        }

        /// <summary>
        /// Returns web elements that have the expected value from a specified property
        /// </summary>
        /// <param name="webElements"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<IWebElement> WhereElementsHavePropertyValue(this IEnumerable<IWebElement> webElements, string propertyName, string value)
        {
            return webElements.Where(element => element.GetDomProperty(propertyName).Equals(value));
        }

        /// <summary>
        /// Move the mouse to the specific element. Scroll element into viewport and return the web element.
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement MoveToElement(this IWebElement webElement, IWebDriver driver)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(webElement).Build().Perform();

            return webElement;
        }
    }
}
