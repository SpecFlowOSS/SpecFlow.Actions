using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium;

public interface IDriverInitialiser
{
    IWebDriver Initialise();
}