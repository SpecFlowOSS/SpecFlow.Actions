using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Example.PageObjects;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Example.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly Actor _actor;

        
        public CalculatorStepDefinitions(Actor actor)
        {
            _actor = actor;
        }

        [BeforeScenario()]
        public void EnsureCalculatorIsOpenAndReset()
        {
            //Open the calculator page in the browser if not opened yet
            if (_actor.AskingFor(CurrentUrl.FromBrowser()) != CalculatorElementLocators.CalculatorUrl)
            {
                _actor.AttemptsTo(Navigate.ToUrl(CalculatorElementLocators.CalculatorUrl));
            }
            //Otherwise reset the calculator by clicking the reset button
            else
            {
                _actor.AttemptsTo(Click.On(CalculatorElementLocators.ResetButtonLocator));
            }
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            _actor.AttemptsTo(Clear.On(CalculatorElementLocators.FirstNumberFieldLocator));
            _actor.AttemptsTo(SendKeys.To(CalculatorElementLocators.FirstNumberFieldLocator, number.ToString()));
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _actor.AttemptsTo(Clear.On(CalculatorElementLocators.SecondNumberFieldLocator));
            _actor.AttemptsTo(SendKeys.To(CalculatorElementLocators.SecondNumberFieldLocator, number.ToString()));
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _actor.AttemptsTo(Click.On(CalculatorElementLocators.AddButtonLocator));
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int expectedResult)
        {
            var actualResult = _actor.WaitsUntil(HtmlAttribute.Of(CalculatorElementLocators.ResultLabelLocator, "value"), IsNot<string?>.Condition(IsNullOrWhitespaceCondition.Value()));

            actualResult.Should().Be(expectedResult.ToString());
        }
    }
}