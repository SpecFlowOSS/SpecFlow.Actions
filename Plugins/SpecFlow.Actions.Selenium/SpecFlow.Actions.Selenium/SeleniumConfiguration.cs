using System;
using System.IO;
using TechTalk.SpecFlow.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium
{
    public interface ISeleniumConfiguration
    {
        string Browser { get; }
    }
    public class SeleniumConfiguration : ISeleniumConfiguration
    {
        private readonly ISpecFlowJsonLoader _specFlowJsonLoader;

        private class SpecFlowJson
        {
            [JsonInclude]
            public SeleniumSpecFlowJsonPart Selenium { get; private set; } = new SeleniumSpecFlowJsonPart();
        }

        private class SeleniumSpecFlowJsonPart
        {
            [JsonInclude]
            public string Browser { get; private set; }
        }


        private readonly Lazy<SpecFlowJson> _specflowJsonPart;

        private SpecFlowJson LoadSpecFlowJson()
        {
            var json = _specFlowJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
                return new SpecFlowJson();

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<SpecFlowJson>(json, jsonSerializerOptions);
        }

        public SeleniumConfiguration(ISpecFlowJsonLoader specFlowJsonLoader)
        {
            _specFlowJsonLoader = specFlowJsonLoader;
            _specflowJsonPart = new Lazy<SpecFlowJson>(LoadSpecFlowJson);
        }
        public string Browser => _specflowJsonPart.Value.Selenium.Browser;
    }
}