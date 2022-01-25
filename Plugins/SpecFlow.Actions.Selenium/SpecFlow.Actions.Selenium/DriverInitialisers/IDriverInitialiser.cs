using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium.DriverInitialisers
{
    public interface IDriverInitialiser
    {
        IWebDriver Initialise();
    }
}