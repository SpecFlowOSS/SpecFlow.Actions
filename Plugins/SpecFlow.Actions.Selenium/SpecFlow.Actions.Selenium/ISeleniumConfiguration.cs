using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface ISeleniumConfiguration
    {
        Browser Browser { get; }

        string[] Arguments { get; }

        Dictionary<string, string> Capabilities { get; }

        double? DefaultTimeout { get; }

        double? PollingInterval { get; }

        string? TestPlatform { get; }
    }
}