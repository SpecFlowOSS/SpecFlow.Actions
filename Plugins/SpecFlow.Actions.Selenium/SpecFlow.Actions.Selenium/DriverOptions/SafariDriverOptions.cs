using OpenQA.Selenium.Safari;
using System;

namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public class SafariDriverOptions : IDriverOptions
    {
        private readonly SafariOptions _safariOptions;

        public SafariDriverOptions()
        {
            _safariOptions = new SafariOptions();
        }

        public bool ImplementsArgs => false;

        public dynamic Value => _safariOptions;

        public void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            _safariOptions.AddAdditionalCapability(capabilityName, capabilityValue);
        }

        public void AddArguments(params string[] argumentsToAdd)
        {
            throw new NotImplementedException("SafariOptions does not implement method AddArguments()");
        }
    }
}