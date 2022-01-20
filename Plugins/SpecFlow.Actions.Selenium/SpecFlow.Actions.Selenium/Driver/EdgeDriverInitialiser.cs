using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class EdgeDriverInitialiser : IDriverInitialiser
    {
        private readonly IDriverFactory _driverFactory;

        public EdgeDriverInitialiser(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        public IWebDriver Initialise()
        {
            return _driverFactory.GetEdgeDriver();
        }
    }
}
