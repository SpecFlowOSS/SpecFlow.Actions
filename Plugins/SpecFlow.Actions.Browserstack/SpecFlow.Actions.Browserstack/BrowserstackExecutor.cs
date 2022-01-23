using System.Text.Json.Serialization;

namespace Specflow.Actions.Browserstack;

internal class BrowserstackExecutor
{
    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("arguments")]
    public Arguments? Arguments { get; set; }
}