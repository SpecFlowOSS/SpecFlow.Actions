using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public static class SelectorExtensions
    {
        public static async Task SendTextAsync(this string selector, IPage page, string keys, PageFillOptions? pageFillOptions = null)
        {
            await page.FillAsync(selector, keys, pageFillOptions);
        }

        public static async Task SendKeystrokesAsync(this string selector, IPage page, string keys, PageTypeOptions? pageTypeOptions = null)
        {
            await page.TypeAsync(selector, keys, pageTypeOptions);
        }

        public static async Task ClickAsync(this string selector, IPage page, PageClickOptions? pageClickOptions = null)
        {
            await page.ClickAsync(selector, pageClickOptions);
        }

        public static async Task<string?> GetValueAttributeAsync(this string selector, IPage page, PageInputValueOptions? pageInputValueOptions = null)
        {
            return await page.InputValueAsync(selector, pageInputValueOptions);
        }

        public static async Task WaitForNonEmptyValue(this string selector, IPage page)
        {
            await page.WaitForFunctionAsync($"document.querySelector(\"{selector}\").value !== \"\"");
        }

        public static async Task WaitForEmptyValue(this string selector, IPage page)
        {
            await page.WaitForFunctionAsync($"document.querySelector(\"{selector}\").value === \"\"");
        }

        public static async Task SelectDropdownOptionAsync(this string selector, IPage page, string value, PageSelectOptionOptions? pageSelectOptionOptions = null)
        {
            await page.SelectOptionAsync(selector, new SelectOptionValue { Value = value }, pageSelectOptionOptions);
        }

        public static async Task SelectDropdownOptionAsync(this string selector, IPage page, int index, PageSelectOptionOptions? pageSelectOptionOptions = null)
        {
            await page.SelectOptionAsync(selector, new SelectOptionValue { Index = index }, pageSelectOptionOptions);
        }
    }
}