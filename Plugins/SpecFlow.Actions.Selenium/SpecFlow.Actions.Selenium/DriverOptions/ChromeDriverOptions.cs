using OpenQA.Selenium.Chrome;

namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public class ChromeDriverOptions : IOptionsWrapper
    {
        private readonly ChromeOptions _chromeOptions;

        public ChromeDriverOptions()
        {
            _chromeOptions = new ChromeOptions();
        }

        public dynamic Value => _chromeOptions;

        public bool ImplementsArgs => true;

        public void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            _chromeOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
        }

        public void AddArguments(params string[] argumentsToAdd)
        {
            _chromeOptions.AddArguments(argumentsToAdd);
        }
    }
}