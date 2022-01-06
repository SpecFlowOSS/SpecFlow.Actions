using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Selenium;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Selenium.Targets.Generation
{
    internal class SeleniumTargetsConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;

        [JsonInclude]
        private readonly Lazy<SpecFlowActionsTargets> _specFlowActionsTargetsJson;

        public SeleniumTargetsConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            Debugger.Launch();
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _specFlowActionsTargetsJson = new Lazy<SpecFlowActionsTargets>(LoadSpecFlowJson);
        }

        public SpecFlowActionsTargets Targets => _specFlowActionsTargetsJson.Value;

        private SpecFlowActionsTargets LoadSpecFlowJson()
        {
            var json = _specFlowActionJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new SpecFlowActionsTargets();
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var specflowActionConfig = JsonSerializer.Deserialize<SpecFlowActionsTargets>(json, jsonSerializerOptions);

            return specflowActionConfig ?? new SpecFlowActionsTargets();
        }
    }
}