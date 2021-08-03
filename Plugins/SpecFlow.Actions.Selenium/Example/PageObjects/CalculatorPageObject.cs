using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;

namespace Example.PageObjects
{
    /// <summary>
    /// Calculator Page Object
    /// </summary>
    public class CalculatorPageObject : CalculatorElementLocators
    {
        private readonly IDriverInteractions _driverInteractions;

        private IWebElement FirstNumber => _driverInteractions.GetElement(FirstNumberFieldLocator);
        private IWebElement SecondNumber => _driverInteractions.GetElement(SecondNumberFieldLocator);
        private IWebElement AddButton => _driverInteractions.GetElement(AddButtonLocator);
        private IWebElement Result => _driverInteractions.GetElement(ResultLabelLocator);
        private IWebElement ResetButton => _driverInteractions.GetElement(ResetButtonLocator);


        public CalculatorPageObject(IDriverInteractions driverInteractions)
        {
            _driverInteractions = driverInteractions;
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
            if (_driverInteractions.GetUrl() != CalculatorUrl)
            {
                _driverInteractions.GoToUrl(CalculatorUrl);
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
            return _driverInteractions.WaitUntil(
                () => Result.GetAttribute("value"),
                result => !string.IsNullOrEmpty(result));
        }

        public string? WaitForEmptyResult()
        {
            //Wait for the result to be empty
            return _driverInteractions.WaitUntil(
                () => Result.GetAttribute("value"),
                result => result == string.Empty);
        }
    }
}