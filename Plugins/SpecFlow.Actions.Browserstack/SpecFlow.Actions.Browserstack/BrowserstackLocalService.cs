using BrowserStack;
using SpecFlow.Actions.Selenium.Configuration;
using System.Linq;

namespace SpecFlow.Actions.Browserstack
{
    public class BrowserstackLocalService : IBrowserstackLocalService
    {
        private readonly BrowserstackConfiguration _configuration;
        private Local _browserStackLocal;

        public BrowserstackLocalService(ISeleniumConfiguration configuration)
        {
            _configuration = (BrowserstackConfiguration)configuration;
            _browserStackLocal = new Local();
        }
        
        public void Start()
        {
            _browserStackLocal.start(_configuration.BrowserstackLocalCapabilities.ToList());
        }

        public void Dispose()
        {
            _browserStackLocal.stop();
        }
    }
}