using Microsoft.Playwright;
using SpecFlow.Actions.Playwright;
using System.Threading.Tasks;

namespace Example.PageObjects
{
    /// <summary>
    /// Calculator Page Object
    /// </summary>
    public class CalculatorPageObject : CalculatorElementLocators
    {
        private readonly Task<IBrowser> _browser;
        private readonly Task<IPage> _page;

        public CalculatorPageObject(BrowserDriver browserDriver)
        {
            _browser = browserDriver.Current;
            _page = CreatePageAsync();

        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browser).NewPageAsync();
        }

        public async Task GoToPage()
        {
            await (await _page).GotoAsync(CalculatorUrl);
        }

        public async Task EnterFirstNumberAsync(string number)
        {
            //Enter text
            await FirstNumberFieldSelector.SendKeysAsync(await _page, number);
        }

        public async Task EnterSecondNumberAsync(string number)
        {
            //Enter text
            await SecondNumberFieldSelector.SendKeysAsync(await _page, number);
        }

        public async Task ClickAddAsync()
        {
            //Click the add button
            await AddButtonSelector.ClickAsync(await _page);
        }

        public async Task EnsureCalculatorIsOpenAndResetAsync()
        {
            //Open the calculator page in the browser if not opened yet
            if ((await _page).Url != CalculatorUrl)
            {
                await GoToPage();
            }
            //Otherwise reset the calculator by clicking the reset button
            else
            {
                //Click the rest button
                await ResetButtonSelector.ClickAsync(await _page);

                //Wait until the result is empty again
                //WaitForEmptyResult();
            }
        }

        public async Task<string?> WaitForNonEmptyResultAsync()
        {
            await (await _page).WaitForFunctionAsync($"document.querySelector('{ResultLabelSelector}').value != '0' ");
            return await (await _page).GetAttributeAsync(ResultLabelSelector, "value");
        }

        //public string? WaitForEmptyResult()
        //{
        //    //Wait for the result to be empty
        //    return _browserInteractions.WaitUntil(
        //        () => Result.GetAttribute("value"),
        //        result => result == string.Empty);
        //}
    }
}