namespace Example.PageObjects
{
    public class CalculatorElementLocators
    {
        //The URL of the calculator to be opened in the browser
        private protected const string CalculatorUrl = "https://specflowoss.github.io/Calculator-Demo/Calculator.html";

        //Finding elements by ID
        private protected string FirstNumberFieldSelector => "#first-number";
        private protected string SecondNumberFieldSelector => "#second-number";
        private protected string AddButtonSelector => "#add-button";
        private protected string ResultLabelSelector => "#result";
        private protected string ResetButtonSelector => "#reset-button";
    }
}