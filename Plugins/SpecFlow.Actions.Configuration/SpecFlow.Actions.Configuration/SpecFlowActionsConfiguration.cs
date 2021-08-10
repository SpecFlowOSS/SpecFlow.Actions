using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

namespace SpecFlow.Actions.Configuration
{
    public interface ISpecFlowActionsConfiguration
    {
        string Get(string path);
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

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder = configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(specflowActionJsonContent)));
            return configurationBuilder.Build();
        }

        public string Get(string path)
        {
            var configValue = _configuration.Value[path];

            if (configValue is null)
            {
                throw new ConfigurationValueNotFoundException(path);
            }

            return configValue;
        }
    }
}