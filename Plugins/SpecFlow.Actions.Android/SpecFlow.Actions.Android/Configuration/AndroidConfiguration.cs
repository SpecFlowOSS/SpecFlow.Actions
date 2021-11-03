using SpecFlow.Actions.Appium.Configuration.Android;
using SpecFlow.Actions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SpecFlow.Actions.Android.Configuration
{
    public class AndroidConfiguration : IAndroidConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;
        private readonly Lazy<SpecFlowActionJson> _specflowJsonPart;

        public AndroidConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
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

        public Dictionary<string, string> Capabilities => _specflowJsonPart.Value.Android.Capabilities;

        public int? Timeout => _specflowJsonPart.Value.Android.Timeout;

        public bool LocalAppiumServerRequired => _specflowJsonPart.Value.AppiumServer.LocalAppiumServerRequired ?? true;

        public Uri? ServerAddress => _specflowJsonPart.Value.AppiumServer.ServerAddress;
    }
}