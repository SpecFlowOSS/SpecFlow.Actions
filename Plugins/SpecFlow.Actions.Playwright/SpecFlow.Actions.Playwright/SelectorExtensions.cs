using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public static class SelectorExtensions
    {
        /// <summary>
        /// Sends a string to the specified selector
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <param name="keys"></param>
        /// <param name="pageFillOptions"></param>
        /// <returns></returns>
        public static async Task SendTextAsync(this string selector, IPage page, string keys, PageFillOptions? pageFillOptions = null)
        {
            await page.FillAsync(selector, keys, pageFillOptions);
        }

        /// <summary>
        /// Sends individual keystrokes to the specified selector
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <param name="keys"></param>
        /// <param name="pageTypeOptions"></param>
        /// <returns></returns>
        public static async Task SendKeystrokesAsync(this string selector, IPage page, string keys, PageTypeOptions? pageTypeOptions = null)
        {
            await page.TypeAsync(selector, keys, pageTypeOptions);
        }

        /// <summary>
        /// Sends a click to an element
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <param name="pageClickOptions"></param>
        /// <returns></returns>
        public static async Task ClickAsync(this string selector, IPage page, PageClickOptions? pageClickOptions = null)
        {
            await page.ClickAsync(selector, pageClickOptions);
        }

        /// <summary>
        /// Gets the value attribute of an element
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <param name="pageInputValueOptions"></param>
        /// <returns></returns>
        public static async Task<string?> GetValueAttributeAsync(this string selector, IPage page, PageInputValueOptions? pageInputValueOptions = null)
        {
            return await page.InputValueAsync(selector, pageInputValueOptions);
        }

        /// <summary>
        /// Waits for the value attribute of an element to not be empty
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task WaitForNonEmptyValue(this string selector, IPage page)
        {
            await page.WaitForFunctionAsync($"document.querySelector(\"{selector}\").value !== \"\"");
        }

        /// <summary>
        /// Waits for the value attribute of an element to be empty
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task WaitForEmptyValue(this string selector, IPage page)
        {
            await page.WaitForFunctionAsync($"document.querySelector(\"{selector}\").value === \"\"");
        }

        /// <summary>
        /// Selects the option from a select element by its value
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <param name="value"></param>
        /// <param name="pageSelectOptionOptions"></param>
        /// <returns></returns>
        public static async Task SelectDropdownOptionAsync(this string selector, IPage page, string value, PageSelectOptionOptions? pageSelectOptionOptions = null)
        {
            await page.SelectOptionAsync(selector, new SelectOptionValue { Value = value }, pageSelectOptionOptions);
        }

        /// <summary>
        /// Selects the option from a select element by its index
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <param name="index"></param>
        /// <param name="pageSelectOptionOptions"></param>
        /// <returns></returns>
        public static async Task SelectDropdownOptionAsync(this string selector, IPage page, int index, PageSelectOptionOptions? pageSelectOptionOptions = null)
        {
            await page.SelectOptionAsync(selector, new SelectOptionValue { Index = index }, pageSelectOptionOptions);
        }
    }
}