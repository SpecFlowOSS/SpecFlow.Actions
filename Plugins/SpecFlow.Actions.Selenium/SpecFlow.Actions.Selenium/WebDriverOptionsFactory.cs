using SpecFlow.Actions.Selenium.DriverOptions;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public class WebDriverOptionsFactory : IWebDriverOptionsFactory
    {
        private readonly IOptionsConfigurator _optionsConfigurator;

        public WebDriverOptionsFactory(IOptionsConfigurator optionsConfigurator)
        {
            _optionsConfigurator = optionsConfigurator;
        }

        public IOptionsWrapper GetChromeOptions(Dictionary<string, string> caps, string[] args)
        {
            var wrapper = new ChromeOptionsWrapper();
            _optionsConfigurator.Add(wrapper, caps, args);
            return wrapper;
        }

        public IOptionsWrapper GetFireFoxOptions(Dictionary<string, string> caps, string[] args)
        {
            var wrapper = new FirefoxOptionsWrapper();
            _optionsConfigurator.Add(wrapper, caps, args);
            return wrapper;
        }

        public IOptionsWrapper GetEdgeOptions(Dictionary<string, string> caps, string[] args) 
        {
            var wrapper = new EdgeOptionsWrapper();
            _optionsConfigurator.Add(wrapper, caps, args);
            return wrapper;
        }

        public IOptionsWrapper GetInternetExplorerOptions(Dictionary<string, string> caps, string[] args) 
        {
            var wrapper = new InternetExplorerOptionsWrapper();
            _optionsConfigurator.Add(wrapper, caps, args);
            return wrapper;
        }

        public IOptionsWrapper GetSafariOptions(Dictionary<string, string> caps, string[] args) 
        {
            var wrapper = new SafariOptionsWrapper();
            _optionsConfigurator.Add(wrapper, caps, args);
            return wrapper;
        }
    }
}