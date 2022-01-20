namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public interface IOptionsWrapper
    {
        dynamic Value { get; }

        bool ImplementsArgs { get; }

        void AddAdditionalCapability(string capabilityName, object capabilityValue);

        void AddArguments(params string[] argumentsToAdd);
    }
}