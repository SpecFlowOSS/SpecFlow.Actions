using OpenQA.Selenium.Edge;
using System;

namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public class EdgeOptionsWrapper : IOptionsWrapper
    {
        private readonly EdgeOptions _edgeOptions;

        public EdgeOptionsWrapper()
        {
            _edgeOptions = new EdgeOptions();
        }

        public dynamic Value => _edgeOptions;

        public bool ImplementsArgs => false;

        public void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            _edgeOptions.AddAdditionalCapability(capabilityName, capabilityValue);
        }

        public void AddArguments(params string[] argumentsToAdd)
        {
            throw new NotImplementedException("EdgeOptions does not implement method AddArguments()");
        }
    }
}