using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium.Driver
{
    internal class FirefoxDriverInitialiser : IDriverInitialiser
    {
        private readonly IDriverFactory _driverFactory;

        public FirefoxDriverInitialiser(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        public IWebDriver Initialise()
        {
            return _driverFactory.GetFirefoxDriver();
        }
    }
}
