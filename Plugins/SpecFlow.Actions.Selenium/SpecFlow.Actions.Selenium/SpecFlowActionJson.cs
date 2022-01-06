using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium
{
    internal class SpecFlowActionJson
    {
        [JsonInclude]
        public SeleniumSpecFlowJsonPart Selenium { get; private set; } = new SeleniumSpecFlowJsonPart();
    }
}