using Example.PageObjects;
using TechTalk.SpecFlow;

namespace Example.Hooks
{
    /// <summary>
    /// Calculator related hooks
    /// </summary>
    [Binding]
    public class CalculatorHooks
    {
        ///<summary>
        ///  Reset the calculator before each scenario tagged with "Calculator"
        /// </summary>
        [BeforeScenario("Calculator")]
        public async void BeforeScenarioAsync(CalculatorPageObject calculatorPageObject)
        {
            await calculatorPageObject.EnsureCalculatorIsOpenAndResetAsync();
        }
    }
}