using SpecFlow.Actions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SpecFlow.Actions.WindowsAppDriver.Configuration
{
    public class WindowsAppDriverConfiguration : IWindowsAppDriverConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;
        private readonly Lazy<SpecFlowActionJson> _specflowJsonPart;

        public WindowsAppDriverConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _specflowJsonPart = new Lazy<SpecFlowActionJson>(LoadSpecFlowJson);
        }

        private SpecFlowActionJson LoadSpecFlowJson()
        {
            var json = _specFlowActionJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new SpecFlowActionJson();
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var specflowActionConfig = JsonSerializer.Deserialize<SpecFlowActionJson>(json, jsonSerializerOptions);

            return specflowActionConfig ?? new SpecFlowActionJson();
        }

        public Dictionary<string, string>? Capabilities => _specflowJsonPart.Value.WindowsAppDriver.Capabilities;

        public string? WindowsAppDriverPath => _specflowJsonPart.Value.WindowsAppDriver.WindowsAppDriverPath;

        public bool? EnableScreenshots => _specflowJsonPart.Value.WindowsAppDriver.EnableScreenshots;
    }
}