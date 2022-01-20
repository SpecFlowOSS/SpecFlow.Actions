using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium.Driver;

public interface IDriverInitialiser
{
    IWebDriver Initialise();
}