using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.WindowsAppDriver.Configuration
{
    public class WindowsAppDriverJsonPart
    {
        [JsonInclude] public Dictionary<string, string> Capabilities { get; set; } = new();

        [JsonInclude] public string? WindowsAppDriverPath { get; set; }

        [JsonInclude] public bool? EnableScreenshots { get; set; }
    }
}