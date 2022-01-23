using System.Text.Json;

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
}