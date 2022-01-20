using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium.Driver
{
    internal class SafariDriverInitialiser : IDriverInitialiser
    {
        private readonly IDriverFactory _driverFactory;

        public SafariDriverInitialiser(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        public IWebDriver Initialise()
        {
            return _driverFactory.GetSafariDriver();
        }
    }
}