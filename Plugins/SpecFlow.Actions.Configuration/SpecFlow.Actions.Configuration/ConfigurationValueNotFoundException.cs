// unset

using System;

namespace SpecFlow.Actions.Configuration
{
    public class ConfigurationValueNotFoundException : Exception
    {
        public ConfigurationValueNotFoundException(string path) : base($"No configuration value found for path {path}")
        {
            
        }
    }
}