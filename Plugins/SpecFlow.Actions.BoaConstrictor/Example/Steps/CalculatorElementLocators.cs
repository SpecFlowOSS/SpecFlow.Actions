using OpenQA.Selenium;
using Boa.Constrictor;
using Boa.Constrictor.Selenium;
using static Boa.Constrictor.Selenium.WebLocator;

namespace Example.Steps
{
    public class CalculatorElementLocators
    {
        //The URL of the calculator to be opened in the browser
        public static string CalculatorUrl = "https://specflowoss.github.io/Calculator-Demo/Calculator.html";
        public static IWebLocator FirstNumberFieldLocator => L("First number", By.Id("first-number"));
        public static IWebLocator SecondNumberFieldLocator => L("second-number", By.Id("second-number"));
        public static IWebLocator AddButtonLocator => L("add-button", By.Id("add-button"));
        public static IWebLocator ResultLabelLocator => L("result", By.Id("result"));
        public static IWebLocator ResetButtonLocator => L("reset", By.Id("reset-button"));
    }
}