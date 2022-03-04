using System.Text.Json;

namespace SpecFlow.Actions.LambdaTest
{
    internal class LambdaTestTestResultExecutor
    {
        internal static string GetResultExecutor(string result, string? reason = null)
        {
            var json = JsonSerializer.Serialize(new LambdaTestExecutor
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