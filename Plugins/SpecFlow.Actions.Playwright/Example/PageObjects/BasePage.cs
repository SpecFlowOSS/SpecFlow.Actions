using Microsoft.Playwright;
using SpecFlow.Actions.Playwright;
using System.Threading.Tasks;

namespace Example.PageObjects
{
    public class BasePage
    {
        public readonly Task<IPage> _page;

        public BasePage(BrowserDriver browserDriver)
        {
            _page = CreatePageAsync(browserDriver.Current);
        }

        private async Task<IPage> CreatePageAsync(Task<IBrowser> browser)
        {
            // Creates a new page instance
            return await (await browser).NewPageAsync();
        }
    }
}