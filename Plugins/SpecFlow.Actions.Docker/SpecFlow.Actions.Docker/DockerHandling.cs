using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using System.IO;
using System.Linq;

namespace SpecFlow.Actions.Docker
{
    public class DockerHandling
    {
        private static ICompositeService? _compositeService;

        public static void DockerComposeUp()
        {
            var dockerComposeFileName = FindDockerComposeFile();

            _compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerComposeFileName)
                .RemoveAllImages()
                .ForceRecreate()
                .RemoveOrphans()
                .Build()
                .Start();

        }

        
        public static void DockerComposeDown()
        {
            _compositeService?.Stop();
            _compositeService?.Dispose();
        }

        private static string FindDockerComposeFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var root = Path.Combine(currentDirectory, "..", "..", "..", "..", "..");

            return Directory.EnumerateFiles(root, "docker-compose.yml", SearchOption.AllDirectories).First();
        }
    }
}