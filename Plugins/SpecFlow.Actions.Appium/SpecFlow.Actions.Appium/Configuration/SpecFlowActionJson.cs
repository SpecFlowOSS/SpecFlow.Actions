using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Appium.Configuration
{
    public class SpecFlowActionJson
    {
        [JsonInclude] public AppiumJsonPart Appium { get; private set; } = new AppiumJsonPart();

        [JsonInclude]  public AppiumServerJsonPart AppiumServer { get; private set; } = new AppiumServerJsonPart();
    }
}
