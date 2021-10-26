using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Appium.Configuration
{
    public class AppiumJsonPart
    {
        [JsonInclude]
        public Dictionary<string, string>? Capabilities { get; set; }

        [JsonInclude]
        public int? Timeout { get; set; }
    }
}