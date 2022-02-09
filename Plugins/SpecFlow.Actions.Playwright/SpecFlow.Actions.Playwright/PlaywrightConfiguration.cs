using SpecFlow.Actions.Configuration;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Playwright
{
    public interface IPlaywrightConfiguration
    {
        Browser Browser { get; }

        string[]? Arguments { get; }

        float? DefaultTimeout { get; }

        bool? Headless { get; }

        float? SlowMo { get; }
        
        string? TraceDir { get; }
    }

    public class PlaywrightConfiguration : IPlaywrightConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;

        private class SpecFlowActionJson
        {
            [JsonInclude]
            public PlaywrightSpecFlowJsonPart Playwright { get; private set; } = new PlaywrightSpecFlowJsonPart();
        }

        private class PlaywrightSpecFlowJsonPart
        {
            [JsonInclude]
            public Browser Browser { get; private set; }

            [JsonInclude]
            public string[]? Arguments { get; private set; }

            [JsonInclude]
            public float? DefaultTimeout { get; private set; }

            [JsonInclude]
            public bool? Headless { get; private set; }

            [JsonInclude]
            public float? SlowMo { get; private set; }

            [JsonInclude]
            public string? TraceDir { get; private set; }
        }

        private readonly Lazy<SpecFlowActionJson> _specflowJsonPart;

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

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var specflowActionConfig = JsonSerializer.Deserialize<SpecFlowActionJson>(json, jsonSerializerOptions);

            return specflowActionConfig ?? new SpecFlowActionJson();
        }

        /// <summary>
        /// Provides the configuration details for the Playwright instance
        /// </summary>
        /// <param name="specFlowActionJsonLoader"></param>
        public PlaywrightConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _specflowJsonPart = new Lazy<SpecFlowActionJson>(LoadSpecFlowJson);
        }

        /// <summary>
        /// The browser specified in the configuration
        /// </summary>
        public Browser Browser => _specflowJsonPart.Value.Playwright.Browser;

        /// <summary>
        /// Additional arguments used when launching the browser
        /// </summary>
        public string[]? Arguments => _specflowJsonPart.Value.Playwright.Arguments;

        /// <summary>
        /// The default timeout used to configure the browser
        /// </summary>
        public float? DefaultTimeout => _specflowJsonPart.Value.Playwright.DefaultTimeout;

        /// <summary>
        /// Whether the browser should launch headless
        /// </summary>
        public bool? Headless => _specflowJsonPart.Value.Playwright.Headless;

        /// <summary>
        /// How many miliseconds elapse between every action 
        /// </summary>
        public float? SlowMo => _specflowJsonPart.Value.Playwright.SlowMo;

        /// <summary>
        /// If specified, traces are saved into this directory 
        /// </summary>
        public string? TraceDir => _specflowJsonPart.Value.Playwright.TraceDir;
    }
}