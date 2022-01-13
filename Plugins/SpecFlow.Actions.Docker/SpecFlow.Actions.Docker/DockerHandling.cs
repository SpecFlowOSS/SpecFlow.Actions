using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using System;
using System.IO;

namespace SpecFlow.Actions.Docker
{
    class DockerHandling : IDockerHandling
    {
        private readonly DockerConfiguration _dockerConfiguration;
        private ICompositeService? _compositeService;

        public DockerHandling(DockerConfiguration dockerConfiguration)
        {
            _dockerConfiguration = dockerConfiguration;
        }

        public void DockerComposeUp()
        {
            if (_dockerConfiguration.File is null)
            {
                throw new DockerConfigurationFileNotFound();
            }

            var dockerConfigurationFile = Path.Combine(Environment.CurrentDirectory, _dockerConfiguration.File);

            if (!File.Exists(dockerConfigurationFile))
            {
                throw new DockerConfigurationFileNotFound(dockerConfigurationFile);
            }

            _compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerConfigurationFile)
                .RemoveAllImages()
                .ForceRecreate()
                .RemoveOrphans()
                .Build()
                .Start();
        }

        
        public void DockerComposeDown()
        {
            _compositeService?.Stop();
            _compositeService?.Dispose();
        }
    }
}