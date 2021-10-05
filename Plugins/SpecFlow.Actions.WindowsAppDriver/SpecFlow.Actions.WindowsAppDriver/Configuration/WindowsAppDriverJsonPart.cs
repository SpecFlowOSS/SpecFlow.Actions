using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.WindowsAppDriver.Configuration
{
    public class WindowsAppDriverJsonPart
    {
        [JsonInclude]
        public Dictionary<string, string>? Capabilities { get; set; }

        [JsonInclude]
        public string? WindowsAppDriverPath { get; set; }
    }
}