using SpecFlow.Actions.Selenium.Enums;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium.Configuration
{
    public interface IBrowserConfiguration
    {
        Browser Browser { get; set; }

        string[]? Arguments { get; set; }

        Dictionary<string, string>? Capabilities { get; set; }
    }
}