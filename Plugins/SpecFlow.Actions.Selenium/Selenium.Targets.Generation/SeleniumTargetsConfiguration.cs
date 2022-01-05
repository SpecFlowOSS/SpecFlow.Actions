using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Selenium;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Selenium.Targets.Generation
{
    internal class SeleniumTargetsConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;

        [JsonInclude]
        private readonly Lazy<SeleniumSpecFlowJsonPart[]> _seleniumSpecFlowJsonParts;

        public SeleniumTargetsConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _seleniumSpecFlowJsonParts = new Lazy<SeleniumSpecFlowJsonPart[]>(LoadSpecFlowJson);
        }

        public SeleniumSpecFlowJsonPart[] Selenium => _seleniumSpecFlowJsonParts.Value;

        private SeleniumSpecFlowJsonPart[] LoadSpecFlowJson()
        {
            var json = _specFlowActionJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
            {
                return Array.Empty<SeleniumSpecFlowJsonPart>();
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var specflowActionConfig = JsonSerializer.Deserialize<SeleniumSpecFlowJsonPart[]>(json, jsonSerializerOptions);

            return specflowActionConfig ?? Array.Empty<SeleniumSpecFlowJsonPart>();
        }
    }
}