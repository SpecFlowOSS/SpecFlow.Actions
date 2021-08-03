using SpecFlow.Actions.Selenium.build;

namespace Example.PageObjects
{
    /// <summary>
    /// Calculator Page Object
    /// </summary>
    public class CalculatorPageObject : CalculatorPageElements
    {
        private readonly IBrowserDriverInteractions _browserDriverInteractions;

        //The URL of the calculator to be opened in the browser
        private const string CalculatorUrl = "https://specflowoss.github.io/Calculator-Demo/Calculator.html";

        public CalculatorPageObject(IBrowserDriverInteractions browserDriverInteractions) : base(browserDriverInteractions)
        {
            _browserDriverInteractions = browserDriverInteractions;
        }

        public void EnterFirstNumber(string number)
        {
            //Clear text box
            FirstNumber.Clear();
            //Enter text
            FirstNumber.SendKeys(number);
        }

        public void EnterSecondNumber(string number)
        {
            //Clear text box
            SecondNumber.Clear();
            //Enter text
            SecondNumber.SendKeys(number);
        }

        public void ClickAdd()
        {
            //Click the add button
            AddButton.Click();
        }

        public void EnsureCalculatorIsOpenAndReset()
        {
            //Open the calculator page in the browser if not opened yet
            if (_browserDriverInteractions.GetUrl() != CalculatorUrl)
            {
                _browserDriverInteractions.GoToUrl(CalculatorUrl);
            }
            //Otherwise reset the calculator by clicking the reset button
            else
            {
                //Click the rest button
                ResetButton.Click();

                //Wait until the result is empty again
                WaitForEmptyResult();
            }
        }

        public string? WaitForNonEmptyResult()
        {
            //Wait for the result to be not empty
            return _browserDriverInteractions.WaitUntil(
                () => Result.GetAttribute("value"),
                result => !string.IsNullOrEmpty(result));
        }

        public string? WaitForEmptyResult()
        {
            //Wait for the result to be empty
            return _browserDriverInteractions.WaitUntil(
                () => Result.GetAttribute("value"),
                result => result == string.Empty);
        }
    }
}