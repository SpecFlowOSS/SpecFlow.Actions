using OpenQA.Selenium.Appium.Android;
using SpecFlow.Actions.Appium;

namespace Example.App
{
    public class CalculatorFormElements
    {
        private readonly AndroidDriver<AndroidElement> _androidDriver;

        public CalculatorFormElements(AppDriver appDriver)
        {
            _androidDriver = (AndroidDriver<AndroidElement>)appDriver.Current;
        }

        public AndroidElement FirstNumberTextBox =>
            _androidDriver.FindElementById("com.companyname.specflowcalculator:id/firstNumberTextBox");

        public AndroidElement SecondNumberTextBox =>
            _androidDriver.FindElementById("com.companyname.specflowcalculator:id/secondNumberTextBox");

        public AndroidElement AddButton =>
            _androidDriver.FindElementById("com.companyname.specflowcalculator:id/addButton");

        public AndroidElement SubtractButton =>
            _androidDriver.FindElementById("com.companyname.specflowcalculator:id/subtractButton");

        public AndroidElement MultiplyButton =>
            _androidDriver.FindElementById("com.companyname.specflowcalculator:id/multiplyButton");

        public AndroidElement DivideButton =>
            _androidDriver.FindElementById("com.companyname.specflowcalculator:id/divideButton");

        public AndroidElement ResultTextBox =>
            _androidDriver.FindElementById("com.companyname.specflowcalculator:id/resultTextBox");
    }
}