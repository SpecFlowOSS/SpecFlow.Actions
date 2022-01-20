using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Configuration;

namespace SpecFlow.Actions.Selenium.DriverInitialisers
{
    internal class FirefoxDriverInitialiser : IDriverInitialiser
    {
        private readonly IDriverFactory _driverFactory;
        private readonly IWebDriverOptionsFactory _webDriverOptionsFactory;
        private readonly ISeleniumConfiguration _seleniumConfiguration;

        public FirefoxDriverInitialiser(IDriverFactory driverFactory, IWebDriverOptionsFactory webDriverOptionsFactory, ISeleniumConfiguration seleniumConfiguration)
        {
            _driverFactory = driverFactory;
            _webDriverOptionsFactory = webDriverOptionsFactory;
            _seleniumConfiguration = seleniumConfiguration;
        }

        public IWebDriver Initialise()
        {
            var options = _webDriverOptionsFactory.GetFireFoxOptions(_seleniumConfiguration.Capabilities,
                _seleniumConfiguration.Arguments);

            return _driverFactory.GetFirefoxDriver(options);
        }
    }
}
