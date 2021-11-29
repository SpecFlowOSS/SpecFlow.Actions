namespace SpecFlow.Actions.Selenium.Configuration
{
    public interface ISeleniumConfiguration
    {
        BrowserConfiguration[] BrowserConfigurations { get; }

        double? DefaultTimeout { get; }

        double? PollingInterval { get; }

        string? TestPlatform { get; }
    }
}