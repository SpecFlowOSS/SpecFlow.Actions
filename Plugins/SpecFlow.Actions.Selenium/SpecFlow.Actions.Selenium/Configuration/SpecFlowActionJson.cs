using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium.Configuration
{
    public class SpecFlowActionJson
    {
        [JsonInclude]
        public SeleniumSpecFlowJsonPart Selenium { get; private set; } = new SeleniumSpecFlowJsonPart();
    }
}