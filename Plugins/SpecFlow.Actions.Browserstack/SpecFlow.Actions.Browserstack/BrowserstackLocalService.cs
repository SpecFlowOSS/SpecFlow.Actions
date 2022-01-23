using BrowserStack;
using SpecFlow.Actions.Selenium.Configuration;
using System.Linq;

namespace SpecFlow.Actions.Browserstack
{
    public class BrowserstackLocalService : IBrowserstackLocalService
    {
        private readonly BrowserstackConfiguration _configuration;

        public BrowserstackLocalService(ISeleniumConfiguration configuration)
        {
            _configuration = (BrowserstackConfiguration)configuration;
        }

        private Local? _browserStackLocal;
        
        public void Start()
        {
            if (_configuration.Capabilities.ContainsKey("local"))
            {
                _browserStackLocal = new Local();
                _browserStackLocal.start(_configuration.BrowserstackLocalCapabilities.ToList());
            }
        }

        public void Dispose()
        {
            _browserStackLocal?.stop();
        }
    }
}