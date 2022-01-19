using BrowserStack;
using SpecFlow.Actions.Selenium;
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
            _browserStackLocal = new Local();
            _browserStackLocal.start(_configuration.BrowserstackLocalCapabilities.ToList());
        }

        public void Dispose()
        {
            if (_browserStackLocal is not null && _browserStackLocal.isRunning())
            {
                _browserStackLocal.stop();
            }
        }
    }
}