using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SpecFlow.Actions.Selenium.Driver
{
    public class ChromeDriverInitialiser : IDriverInitialiser
    {
        private readonly IDriverFactory _driverFactory;

        public ChromeDriverInitialiser(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        public IWebDriver Initialise()
        {
            return _driverFactory.GetChromeDriver();
        }
    }

    public class ChromeDriverLocalInitialiser : IDriverInitialiser
    {
        public IWebDriver Initialise()
        {
            //var options = _driverOptionsFactory.GetChromeOptions();

            //return string.IsNullOrWhiteSpace(_chromeWebDriverFilePath.Value)
            //    ? new ChromeDriver(ChromeDriverService.CreateDefaultService(), options.Value, TimeSpan.FromSeconds(120))
            //    : new ChromeDriver(ChromeDriverService.CreateDefaultService(_chromeWebDriverFilePath.Value), options.Value, TimeSpan.FromSeconds(120));
            throw new NotImplementedException();
        }
    }
}