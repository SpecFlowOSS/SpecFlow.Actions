using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium
{
    public interface ISeleniumConfiguration
    {
        Browser Browser { get; }

        string[] Arguments { get; }
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
            public Browser Browser { get; private set; }

            [JsonInclude]
            public string[] Arguments { get; private set; }
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

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<SpecFlowJson>(json, jsonSerializerOptions);
        }

        public SeleniumConfiguration(ISpecFlowJsonLoader specFlowJsonLoader)
        {
            _specFlowJsonLoader = specFlowJsonLoader;
            _specflowJsonPart = new Lazy<SpecFlowJson>(LoadSpecFlowJson);
        }

        public Browser Browser => _specflowJsonPart.Value.Selenium.Browser;

        public string[] Arguments => _specflowJsonPart.Value.Selenium.Arguments;
    }
}