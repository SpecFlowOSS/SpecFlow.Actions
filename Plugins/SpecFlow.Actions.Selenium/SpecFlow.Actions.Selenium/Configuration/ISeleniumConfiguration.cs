namespace SpecFlow.Actions.Selenium.Configuration
{
    public interface ISeleniumConfiguration
    {
        TargetConfiguration[] Targets { get; }

        double? DefaultTimeout { get; }

        double? PollingInterval { get; }

        string? TestPlatform { get; }
    }
}