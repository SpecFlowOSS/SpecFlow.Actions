using SpecFlow.Actions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SpecFlow.Actions.Appium.Configuration
{
    public class AppiumConfiguration : IAppiumConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;
        private readonly Lazy<SpecFlowActionJson> _specflowJsonPart;

        internal AppiumConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
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

        public Dictionary<string, string>? Capabilities => _specflowJsonPart.Value.Appium.Capabilities;

        public int? Timeout => _specflowJsonPart.Value.Appium.Timeout;

        public bool LocalAppiumServerRequired => _specflowJsonPart.Value.AppiumServer.LocalAppiumServerRequired ?? true;

        public Uri? ServerAddress => _specflowJsonPart.Value.AppiumServer.ServerAddress;
    }
}