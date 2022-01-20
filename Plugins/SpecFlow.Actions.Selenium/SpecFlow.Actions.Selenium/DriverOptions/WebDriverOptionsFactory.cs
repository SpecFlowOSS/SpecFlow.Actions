namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public class WebDriverOptionsFactory : IWebDriverOptionsFactory
    {
        private readonly IOptionsConfigurator _optionsConfigurator;

        public WebDriverOptionsFactory(IOptionsConfigurator optionsConfigurator)
        {
            _optionsConfigurator = optionsConfigurator;
        }

        public IOptionsWrapper GetChromeOptions()
        {
            var options = new ChromeOptionsWrapper();
            _optionsConfigurator.Add(options);
            return options;
        }

        public IOptionsWrapper GetFireFoxOptions()
        {
            var options = new FirefoxOptionsWrapper();
            _optionsConfigurator.Add(options);
            return options;
        }

        public IOptionsWrapper GetEdgeOptions()
        {
            var options = new EdgeOptionsWrapper();
            _optionsConfigurator.Add(options);
            return options;
        }

        public IOptionsWrapper GetInternetExplorerOptions()
        {
            var options = new InternetExplorerOptionsWrapper();
            _optionsConfigurator.Add(options);
            return options;
        }

        public IOptionsWrapper GetSafariOptions()
        {
            var options = new SafariOptionsWrapper();
            _optionsConfigurator.Add(options);
            return options;
        }
    }
}