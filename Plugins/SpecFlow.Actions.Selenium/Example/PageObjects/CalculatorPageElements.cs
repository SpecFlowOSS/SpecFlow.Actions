using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.build;

namespace Example.PageObjects
{
    public class CalculatorPageElements
    {
        private readonly IBrowserDriverInteractions _browserDriverInteractions;

        public CalculatorPageElements(IBrowserDriverInteractions browserDriverInteractions)
        {
            _browserDriverInteractions = browserDriverInteractions;
        }

        //Finding elements by ID
        private protected IWebElement FirstNumber => _browserDriverInteractions.GetElement(By.Id("first-number"));
        private protected IWebElement SecondNumber => _browserDriverInteractions.GetElement(By.Id("second-number"));
        private protected IWebElement AddButton => _browserDriverInteractions.GetElement(By.Id("add-button"));
        private protected IWebElement Result => _browserDriverInteractions.GetElement(By.Id("result"));
        private protected IWebElement ResetButton => _browserDriverInteractions.GetElement(By.Id("reset-button"));
    }
}
