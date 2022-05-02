using System.Text.Json.Serialization;

namespace SpecFlow.Actions.LambdaTest
{

    internal class LambdaTestExecutor
    {
        [JsonPropertyName("action")] public string? Action { get; set; }

        [JsonPropertyName("arguments")] public Arguments? Arguments { get; set; }
    }
}