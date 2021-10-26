using System;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Appium.Configuration
{
    public class AppiumServerJsonPart
    {
        [JsonInclude]
        public bool? LocalAppiumServerRequired { get; set; }

        [JsonInclude]
        public Uri? ServerAddress { get; set; }
    }
}