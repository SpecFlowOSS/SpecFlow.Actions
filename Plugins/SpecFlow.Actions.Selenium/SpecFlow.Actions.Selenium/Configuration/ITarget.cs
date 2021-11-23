using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium.Configuration
{
    public interface ITarget
    {
        Browser Browser { get; set; }

        string[]? Arguments { get; set; }

        Dictionary<string, string>? Capabilities { get; set; }
    }
}