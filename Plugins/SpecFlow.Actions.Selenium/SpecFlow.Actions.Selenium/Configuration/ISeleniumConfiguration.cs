using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium.Configuration
{
    public interface ISeleniumConfiguration
    {
        List<Target> Targets { get; }

        double? DefaultTimeout { get; }

        double? PollingInterval { get; }

        string? TestPlatform { get; }
    }
}