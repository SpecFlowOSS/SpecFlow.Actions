using OpenQA.Selenium.IE;

namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public class InternetExplorerOptionsWrapper : IOptionsWrapper
    {
        private readonly InternetExplorerOptions _internetExplorerOptions;

        public InternetExplorerOptionsWrapper()
        {
            _internetExplorerOptions = new InternetExplorerOptions();
        }

        public dynamic Value => _internetExplorerOptions;

        public bool ImplementsArgs => false;

        public void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            _internetExplorerOptions.AddAdditionalCapability(capabilityName, capabilityValue);
        }

        public void AddArguments(params string[] argumentsToAdd)
        {
            throw new System.NotImplementedException("InternetExplorerOptions does not implement method AddArguments()");
        }
    }
}