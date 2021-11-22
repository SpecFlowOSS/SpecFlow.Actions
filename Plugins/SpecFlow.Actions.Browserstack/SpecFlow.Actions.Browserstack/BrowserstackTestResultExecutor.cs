using System.Text.Json;
using System.Text.Json.Serialization;

namespace Specflow.Actions.Browserstack
{
    internal class BrowserstackTestResultExecutor
    {
        internal static string GetResultExecutor(string result, string? reason = null)
        {
            var json = JsonSerializer.Serialize(new BrowserstackExecutor
            {
                Action = "setSessionStatus", 
                Arguments = new Arguments
                {
                    Status = result,
                    Reason = reason
                }
            });

            return $"browserstack_executor: {json}";
        }
    }

    internal class BrowserstackExecutor
    {
        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("arguments")]
        public Arguments? Arguments { get; set; }
    }

    internal class Arguments
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }
}