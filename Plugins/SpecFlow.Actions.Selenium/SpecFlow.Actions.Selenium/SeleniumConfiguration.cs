using SpecFlow.Actions.Configuration;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium
{
    public interface ISeleniumConfiguration
    {
        Browser Browser { get; }

        string[]? Arguments { get; }
    }

    public class SeleniumConfiguration : ISeleniumConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;

        private class SpecFlowActionJson
        {
            [JsonInclude]
            public SeleniumSpecFlowJsonPart Selenium { get; private set; } = new SeleniumSpecFlowJsonPart();
        }

        private class SeleniumSpecFlowJsonPart
        {
            [JsonInclude]
            public Browser Browser { get; private set; }

            [JsonInclude]
            public string[]? Arguments { get; private set; }
        }

        private readonly Lazy<SpecFlowActionJson> _specflowJsonPart;

        private SpecFlowActionJson LoadSpecFlowJson()
        {
            var json = _specFlowActionJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
                return new SpecFlowActionJson();

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var specflowActionConfig = JsonSerializer.Deserialize<SpecFlowActionJson>(json, jsonSerializerOptions);

            return specflowActionConfig ?? new SpecFlowActionJson();
        }

        public SeleniumConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _specflowJsonPart = new Lazy<SpecFlowActionJson>(LoadSpecFlowJson);
        }

        public Browser Browser => _specflowJsonPart.Value.Selenium.Browser; 

        public string[]? Arguments => _specflowJsonPart.Value.Selenium.Arguments;
    }
}