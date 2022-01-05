using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium
{
    public partial class SeleniumConfiguration
    {
        private class SpecFlowActionJson
        {
            [JsonInclude]
            public SeleniumSpecFlowJsonPart Selenium { get; private set; } = new SeleniumSpecFlowJsonPart();
        }
    }
}