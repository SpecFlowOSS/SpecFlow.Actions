using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Configuration;

namespace SpecFlow.Actions.Selenium.Factories
{
    public interface IDriverFactory
    {
        IWebDriver GetDriver(ITargetConfiguration targetConfiguration);
    }
}