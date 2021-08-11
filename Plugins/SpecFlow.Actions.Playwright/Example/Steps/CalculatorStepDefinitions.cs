using Example.PageObjects;
using FluentAssertions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Example.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        //Page Object for Calculator
        private readonly CalculatorPageObject _calculatorPageObject;

        public CalculatorStepDefinitions(CalculatorPageObject calculatorPageObject)
        {
            _calculatorPageObject = calculatorPageObject;
        }

        [Given("the first number is (.*)")]
        public async Task GivenTheFirstNumberIsAsync(int number)
        {
            //delegate to Page Object
            await _calculatorPageObject.EnterFirstNumberAsync(number.ToString());
        }

        [Given("the second number is (.*)")]
        public async Task GivenTheSecondNumberIsAsync(int number)
        {
            //delegate to Page Object
            await _calculatorPageObject.EnterSecondNumberAsync(number.ToString());
        }

        [When("the two numbers are added")]
        public async Task WhenTheTwoNumbersAreAddedAsync()
        {
            //delegate to Page Object
            await _calculatorPageObject.ClickAddAsync();
        }

        [Then("the result should be (.*)")]
        public async Task ThenTheResultShouldBeAsync(int expectedResult)
        {
            //delegate to Page Object
            var actualResult = await _calculatorPageObject.WaitForNonEmptyResultAsync();

            actualResult.Should().Be(expectedResult.ToString());
        }
    }
}