using BoDi;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public class WebDriverOptions : IWebDriverOptions
    {
        private readonly ISeleniumConfiguration _seleniumConfiguration;
        private readonly IObjectContainer _objectContainer;

        public WebDriverOptions(ISeleniumConfiguration seleniumConfiguration, IObjectContainer objectContainer)
        {
            _seleniumConfiguration = seleniumConfiguration;
            _objectContainer = objectContainer;
        }

        public IOptionsWrapper GetOptions(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            IOptionsWrapper options = browser switch
            {
                Browser.Chrome => new ChromeOptionsWrapper(),
                Browser.Firefox => new FirefoxOptionsWrapper(),
                Browser.Edge => new EdgeOptionsWrapper(),
                Browser.InternetExplorer => new InternetExplorerOptionsWrapper(),
                Browser.Safari => new SafariOptionsWrapper(),
                _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null),
            };

            var optionsConfigurator = _objectContainer.Resolve<IOptionsConfigurator>(_seleniumConfiguration.TestPlatform);

            optionsConfigurator.Add(options, capabilities, args);

            return options;
        }
    }
}