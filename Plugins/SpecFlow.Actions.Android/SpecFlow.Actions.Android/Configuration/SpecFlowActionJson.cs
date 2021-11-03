using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Android.Configuration
{
    public class SpecFlowActionJson
    {
        [JsonInclude] public AndroidJsonPart Android { get; private set; } = new AndroidJsonPart();

        [JsonInclude] public AppiumServerJsonPart AppiumServer { get; private set; } = new AppiumServerJsonPart();
    }
}
