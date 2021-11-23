using SpecFlow.Actions.Selenium.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium.Configuration
{
    public class TargetConfiguration : ITargetConfiguration
    {
        [JsonInclude]
        public Browser Browser { get; set; }

        [JsonInclude]
        public string[]? Arguments { get; set; }

        [JsonInclude]
        public Dictionary<string, string>? Capabilities { get; set; }
    }
}