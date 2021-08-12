using Microsoft.Playwright;
using SpecFlow.Actions.Playwright;
using System.Threading.Tasks;

namespace Example.PageObjects
{
    /// <summary>
    /// Calculator Page Object
    /// </summary>
    public class CalculatorPageObject
    {
        // The page URL
        private protected const string CalculatorUrl = "https://specflowoss.github.io/Calculator-Demo/Calculator.html";

        //Finding elements by ID
        private static string FirstNumberFieldSelector => "#first-number";
        private static string SecondNumberFieldSelector => "#second-number";
        private static string AddButtonSelector => "#add-button";
        private static string ResultLabelSelector => "#result";
        private static string ResetButtonSelector => "#reset-button";

        private readonly Task<IPage> _page;

        public CalculatorPageObject(BrowserDriver browserDriver)
        {
            _page = CreatePageAsync(browserDriver.Current);
        }

        private async Task<IPage> CreatePageAsync(Task<IBrowser> browser)
        {
            // Creates a new page instance
            return await (await browser).NewPageAsync();
        }

        public async Task GoToPage()
        {
            // Opens the calculator page
            await (await _page).GotoAsync(CalculatorUrl);
        }

        public async Task EnterFirstNumberAsync(string number)
        {
            //Enter text
            await FirstNumberFieldSelector.SendTextAsync(await _page, number);
        }

        public async Task EnterSecondNumberAsync(string number)
        {
            //Enter text
            await SecondNumberFieldSelector.SendTextAsync(await _page, number);
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
                await WaitForEmptyResultAsync();
            }
        }

        public async Task<string?> WaitForNonEmptyResultAsync()
        {
            // Waits for the ResultLabelSelector value to be !== ""
            await ResultLabelSelector.WaitForNonEmptyValue(await _page);

            // Gets the value attribute of the ResultLabelSelector
            return await ResultLabelSelector.GetValueAttributeAsync(await _page);
        }

        public async Task WaitForEmptyResultAsync()
        {
            // Waits for the ResultLabelSelector value to be === ""
            await ResultLabelSelector.WaitForEmptyValue(await _page);
        }
    }
}