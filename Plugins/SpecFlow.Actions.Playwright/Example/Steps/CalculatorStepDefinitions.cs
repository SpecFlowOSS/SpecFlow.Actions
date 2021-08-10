using Example.PageObjects;
using FluentAssertions;
using SpecFlow.Actions.Playwright;
using TechTalk.SpecFlow;

namespace Example.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        //Page Object for Calculator
        private readonly CalculatorPageObject _calculatorPageObject;

        public CalculatorStepDefinitions(IBrowserInteractions browserInteractions)
        {
            _calculatorPageObject = new CalculatorPageObject(browserInteractions);
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterFirstNumberAsync(number.ToString());
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            //delegate to Page Object
            _calculatorPageObject.EnterSecondNumberAsync(number.ToString());
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //delegate to Page Object
            _calculatorPageObject.ClickAddAsync();
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int expectedResult)
        {
            //delegate to Page Object
            var actualResult = _calculatorPageObject.WaitForNonEmptyResult();

            actualResult.Should().Be(expectedResult.ToString());
        }
    }
}