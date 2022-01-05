using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium
{
    public record SeleniumSpecFlowJsonPart()
    {
        [JsonInclude]
        public Browser Browser { get; private set; }

        [JsonInclude]
        public string[]? Arguments { get; private set; }

        [JsonInclude]
        public Dictionary<string, string>? Capabilities { get; private set; }

        [JsonInclude]
        public double? DefaultTimeout { get; private set; }

        [JsonInclude]
        public double? PollingInterval { get; private set; }

        [JsonInclude]
        public string? TestPlatform { get; private set; }
    }
}