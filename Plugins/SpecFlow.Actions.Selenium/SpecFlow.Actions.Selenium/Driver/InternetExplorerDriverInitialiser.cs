using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium.Driver
{
    internal class InternetExplorerDriverInitialiser : IDriverInitialiser
    {
        private readonly IDriverFactory _driverFactory;

        public InternetExplorerDriverInitialiser(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        public IWebDriver Initialise()
        {
            return _driverFactory.GetInternetExplorerDriver();
        }
    }
}