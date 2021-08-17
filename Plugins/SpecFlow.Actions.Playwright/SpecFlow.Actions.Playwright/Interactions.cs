using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public class Interactions
    {
        private readonly Task<IPage> _page;

        public Interactions(Task<IPage> page)
        {
            _page = page;
        }

        /// <summary>
        /// Navigates to the specified URL
        /// </summary>
        /// <param name="url"></param>
        public async Task GoToUrl(string url)
        {
            await (await _page).GotoAsync(url);
        }

        /// <summary>
        /// Sends a string to the specified selector
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="keys"></param>
        /// <param name="pageFillOptions"></param>
        /// <returns></returns>
        public async Task SendTextAsync(string selector, string keys, PageFillOptions? pageFillOptions = null)
        {
            await (await _page).FillAsync(selector, keys, pageFillOptions);
        }

        /// <summary>
        /// Sends individual keystrokes to the specified selector
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="keys"></param>
        /// <param name="pageTypeOptions"></param>
        /// <returns></returns>
        public async Task SendKeystrokesAsync(string selector, string keys, PageTypeOptions? pageTypeOptions = null)
        {
            await (await _page).TypeAsync(selector, keys, pageTypeOptions);
        }

        /// <summary>
        /// Sends a click to an element
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="pageClickOptions"></param>
        /// <returns></returns>
        public async Task ClickAsync(string selector, PageClickOptions? pageClickOptions = null)
        {
            await (await _page).ClickAsync(selector, pageClickOptions);
        }

        /// <summary>
        /// Gets the value attribute of an element
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="pageInputValueOptions"></param>
        /// <returns></returns>
        public async Task<string?> GetValueAttributeAsync(string selector, PageInputValueOptions? pageInputValueOptions = null)
        {
            return await (await _page).InputValueAsync(selector, pageInputValueOptions);
        }

        /// <summary>
        /// Waits for the value attribute of an element to not be empty
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task WaitForNonEmptyValue(string selector)
        {
            await (await _page).WaitForFunctionAsync($"document.querySelector(\"{selector}\").value !== \"\"");
        }

        /// <summary>
        /// Waits for the value attribute of an element to be empty
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task WaitForEmptyValue(string selector)
        {
            await (await _page).WaitForFunctionAsync($"document.querySelector(\"{selector}\").value === \"\"");
        }

        /// <summary>
        /// Selects the option from a select element by its value
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        /// <param name="pageSelectOptionOptions"></param>
        /// <returns></returns>
        public async Task SelectDropdownOptionAsync(string selector, string value, PageSelectOptionOptions? pageSelectOptionOptions = null)
        {
            await (await _page).SelectOptionAsync(selector, new SelectOptionValue { Value = value }, pageSelectOptionOptions);
        }

        /// <summary>
        /// Selects the option from a select element by its index
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="index"></param>
        /// <param name="pageSelectOptionOptions"></param>
        /// <returns></returns>
        public async Task SelectDropdownOptionAsync(string selector, int index, PageSelectOptionOptions? pageSelectOptionOptions = null)
        {
            await (await _page).SelectOptionAsync(selector, new SelectOptionValue { Index = index }, pageSelectOptionOptions);
        }
    }
}