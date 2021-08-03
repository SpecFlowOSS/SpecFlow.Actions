using Example.PageObjects;
using SpecFlow.Actions.Selenium.build;
using TechTalk.SpecFlow;

namespace Example.Hooks
{
    /// <summary>
    /// Calculator related hooks
    /// </summary>
    [Binding]
    public class CalculatorHooks
    {
        private readonly IBrowserDriverInteractions _browserDriverInteractions;

        public CalculatorHooks(IBrowserDriverInteractions browserDriverInteractions)
        {
            _browserDriverInteractions = browserDriverInteractions;
        }

        ///<summary>
        ///  Reset the calculator before each scenario tagged with "Calculator"
        /// </summary>
        [BeforeScenario("Calculator")]
        public void BeforeScenario()
        {
            var calculatorPageObject = new CalculatorPageObject(_browserDriverInteractions);
            calculatorPageObject.EnsureCalculatorIsOpenAndReset();
        }
    }
}