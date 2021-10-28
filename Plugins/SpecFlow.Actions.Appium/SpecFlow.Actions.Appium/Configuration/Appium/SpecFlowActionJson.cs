using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Appium.Configuration.Appium
{
    public class SpecFlowActionJson
    {
        [JsonInclude] public AppiumJsonPart Appium { get; private set; } = new AppiumJsonPart();

        [JsonInclude]  public AppiumServerJsonPart AppiumServer { get; private set; } = new AppiumServerJsonPart();
    }
}
