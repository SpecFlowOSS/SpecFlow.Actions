using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpecFlow.Actions.Configuration
{
    public interface ISpecFlowActionsConfiguration
    {
        string? Get(string path);
        string Get(string path, string defaultValue);
        double? GetDouble(string path);
        string[]? GetArray(string path);
        Dictionary<string, string> GetDictionary(string path);
    }

    public class SpecFlowActionsConfiguration : ISpecFlowActionsConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;
        private readonly Lazy<IConfigurationRoot> _configuration;


        public SpecFlowActionsConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;

            _configuration = new Lazy<IConfigurationRoot>(LoadConfiguration);
        }

        private IConfigurationRoot LoadConfiguration()
        {
            var specflowActionJsonContent = _specFlowActionJsonLoader.Load();
            var specflowActionTargetJsonContent = _specFlowActionJsonLoader.LoadTarget();

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder = configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(specflowActionJsonContent)));
            configurationBuilder = configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(specflowActionTargetJsonContent)));
            return configurationBuilder.Build();
        }

        public string? Get(string path)
        {
            var configValue = _configuration.Value[path];
            return configValue;
        }

        public double? GetDouble(string path)
        {
            var configValue = _configuration.Value[path];
            if (configValue != null)
            {
                return Convert.ToDouble(configValue);
            }

            return null;
        }

        public string Get(string path, string defaultValue)
        {
            var configValue = _configuration.Value[path];

            if (configValue is null)
            {
                return defaultValue;
            }

            return configValue;
        }

        public string[]? GetArray(string path)
        {
            return _configuration.Value.GetSection(path).Get<string[]>();
        }

        public Dictionary<string, string> GetDictionary(string path)
        {
            var configurationSection = _configuration.Value.GetSection(path);

            if (configurationSection is null)
            {
                return new Dictionary<string, string>();
            }

            var dictionary = new Dictionary<string, string>();
            foreach (var child in configurationSection.GetChildren())
            {
                dictionary[child.Key] = child.Value;
            }

            return dictionary;
        }
    }
}