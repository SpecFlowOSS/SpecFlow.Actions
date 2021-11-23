using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium.Configuration
{
    public class SeleniumSpecFlowJsonPart
    {
        [JsonInclude]
        public double? DefaultTimeout { get; private set; }

        [JsonInclude]
        public double? PollingInterval { get; private set; }

        [JsonInclude]
        public string? TestPlatform { get; private set; }

        [JsonInclude]
        public List<Target>? Targets { get; private set; }
    }
}