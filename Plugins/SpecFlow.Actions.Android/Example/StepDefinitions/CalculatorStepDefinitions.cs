using Example.App;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Example.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly CalculatorActions _calculatorActions;

        public CalculatorStepDefinitions(CalculatorActions calculatorActions)
        {
            _calculatorActions = calculatorActions;
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            _calculatorActions.EnterFirstNumber(number.ToString());
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _calculatorActions.EnterSecondNumber(number.ToString());
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _calculatorActions.ClickAdd();
        }

        [When(@"the two numbers are subtracted")]
        public void WhenTheTwoNumbersAreSubtracted()
        {
            _calculatorActions.ClickSubtract();
        }

        [When(@"the two numbers are multiplied")]
        public void WhenTheTwoNumbersAreMultiplied()
        {
            _calculatorActions.ClickMultiply();
        }

        [When(@"the two numbers are divided")]
        public void WhenTheTwoNumbersAreDivided()
        {
            _calculatorActions.ClickDivide();
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            var actualResult = int.Parse(_calculatorActions.GetResult());

            result.Should().Be(actualResult);
        }
    }
}