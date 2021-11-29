using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium.Configuration
{
    public class SeleniumSpecFlowJsonPart
    {
        [JsonInclude]
        public double? DefaultTimeout { get; private set; }

        [JsonInclude]
        public double? PollingInterval { get; private set; }

        [JsonInclude]
        public string? TestPlatform { get; private set; }

        [JsonInclude]
        public BrowserConfiguration[] BrowserConfigurations { get; private set; } = new BrowserConfiguration[0];
    }
}