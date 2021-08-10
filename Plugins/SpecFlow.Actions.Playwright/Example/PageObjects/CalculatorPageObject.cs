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
        private readonly IBrowser _browser;
        private readonly IPage _page;

        public CalculatorPageObject(IBrowser browser, BrowserNewPageOptions? browserNewPageOptions = null)
        {
            _browser = browser;
            _page = browser.NewPageAsync(browserNewPageOptions).Result;
        }

        public async Task GoToPage()
        {
            await _page.GotoAsync(CalculatorUrl);
        }

        public async Task EnterFirstNumberAsync(string number)
        {
            //Enter text
            await FirstNumberFieldSelector.SendKeysAsync(_page, number);
        }

        public async Task EnterSecondNumberAsync(string number)
        {
            //Enter text
            await SecondNumberFieldSelector.SendKeysAsync(_page, number);
        }

        public async Task ClickAddAsync()
        {
            //Click the add button
            await AddButtonSelector.ClickAsync(_page);
        }

        public async Task EnsureCalculatorIsOpenAndResetAsync()
        {
            //Open the calculator page in the browser if not opened yet
            if (_page.Url != CalculatorUrl)
            {
                await GoToPage();
            }
            //Otherwise reset the calculator by clicking the reset button
            else
            {
                //Click the rest button
                await ResetButtonSelector.ClickAsync(_page);

                //Wait until the result is empty again
                WaitForEmptyResult();
            }
        }

        public string? WaitForNonEmptyResult()
        {
            _page.WaitForSelectorAsync(ResultLabelSelector, new PageWaitForSelectorOptions{State = } )
            //Wait for the result to be not empty
            //return _browserInteractions.WaitUntil(
            //    () => Result.GetAttribute("value"),
            //    result => !string.IsNullOrEmpty(result));
        }

        public string? WaitForEmptyResult()
        {
            ////Wait for the result to be empty
            //return _browserInteractions.WaitUntil(
            //    () => Result.GetAttribute("value"),
            //    result => result == string.Empty);
        }
    }
}