using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public static class WebElementExtensions
    {
        public static async Task SendKeysAsync(this string selector, IPage page, string keys, PageFillOptions? pageFillOptions = null)
        {
            await page.FillAsync(selector, keys, pageFillOptions);
        }

        public static async Task TypeKeysAsync(this string selector, IPage page, string keys, PageTypeOptions? pageTypeOptions = null)
        {
            await page.TypeAsync(selector, keys, pageTypeOptions);
        }

        public static async Task SelectDropdownOptionAsync(this string selector, IPage page, string option, PageSelectOptionOptions? pageSelectOptionOptions = null)
        {
            await page.SelectOptionAsync(selector, option, pageSelectOptionOptions);
        }

        public static async Task ClickAsync(this string selector, IPage page, PageClickOptions? pageClickOptions = null)
        {
            await page.ClickAsync(selector, pageClickOptions);
        }

        public static async Task<string?> GetValueAttributeAsync(this string selector, IPage page, PageClickOptions? pageClickOptions = null)
        {
            var jsHandle = await page.WaitForSelectorAsync(selector);
            return await jsHandle!.GetAttributeAsync("value");
        }

        //public static void SelectDropdownOptionByIndex(this IPage page, string selector, int index)
        //{
        //    var x= await page.QuerySelectorAsync(selector);
        //    x.
        //}

        ///// <summary>
        ///// Selects an option from a select element by text
        ///// </summary>
        ///// <param name="webElement"></param>
        ///// <param name="text"></param>
        //public static void SelectDropdownOptionByText(this IPage page, string text)
        //{
        //    SelectDropdownOptionAsync(webElement).SelectByText(text);
        //}

        ///// <summary>
        ///// Selects an option from a select element by its value
        ///// </summary>
        ///// <param name="webElement"></param>
        ///// <param name="value"></param>
        //public static void SelectDropdownOptionByValue(this IPage page, string value)
        //{
        //    SelectDropdownOptionAsync(webElement).SelectByText(value);
        //}

        ///// <summary>
        ///// Selects a random dropdown option
        ///// </summary>
        ///// <param name="webElement"></param>
        //public static void SelectRandomDropdownOption(this IPage page)
        //{
        //    var selectElement = SelectDropdownOptionAsync(webElement);
        //    var random = new Random().Next(selectElement.Options.Count - 1);
        //    selectElement.SelectByIndex(random);
        //}

        ///// <summary>
        ///// Finds and returns web elements that contain the specified class name
        ///// </summary>
        ///// <param name="webElements"></param>
        ///// <param name="className"></param>
        ///// <returns></returns>
        //public static IEnumerable<IWebElement> WhereElementsHaveClass(this IEnumerable<IWebElement> webElements, string className)
        //{
        //    return webElements.Where(element => element.GetAttribute("class").Contains(className));
        //}

        ///// <summary>
        ///// Finds and returns web elements that have the specified value
        ///// </summary>
        ///// <param name="webElements"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static IEnumerable<IWebElement> WhereElementsHaveValue(this IEnumerable<IWebElement> webElements, string value)
        //{
        //    return webElements.Where(element => element.GetAttribute("value").Equals(value));
        //}

        ///// <summary>
        ///// Checks if the web element contains a specified class name
        ///// </summary>
        ///// <param name="webElement"></param>
        ///// <param name="className"></param>
        ///// <returns></returns>
        //public static bool HasClass(this IPage page, string className)
        //{
        //    return webElement.GetAttribute("class").Contains(className);
        //}

        ///// <summary>

        ///// <summary>
        ///// Returns only web elements that are displayed
        ///// </summary>
        ///// <param name="webElements"></param>
        ///// <returns></returns>
        //public static IEnumerable<IWebElement> WhereElementsAreDisplayed(this IEnumerable<IWebElement> webElements)
        //{
        //    return webElements.Where(element => element.Displayed.Equals(true));
        //}

        ///// <summary>
        ///// Returns web elements that have the expected value from a specified property
        ///// </summary>
        ///// <param name="webElements"></param>
        ///// <param name="propertyName"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static IEnumerable<IWebElement> WhereElementsHavePropertyValue(this IEnumerable<IWebElement> webElements, string propertyName, string value)
        //{
        //    return webElements.Where(element => element.GetProperty(propertyName).Equals(value));
        //}
    }
}