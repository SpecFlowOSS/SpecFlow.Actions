using System;

namespace SpecFlow.Actions.Docker
{
    public class DockerConfigurationFileNotFound : Exception
    {
        public DockerConfigurationFileNotFound(string dockerConfigurationFile) :base($"The Docker configuration file at '{dockerConfigurationFile}' wasn't found")
        {
            
        }
    }
}