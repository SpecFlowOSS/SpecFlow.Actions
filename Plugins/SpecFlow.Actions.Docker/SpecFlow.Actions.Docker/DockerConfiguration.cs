// unset

using SpecFlow.Actions.Configuration;

namespace SpecFlow.Actions.Docker
{
    public interface IDockerConfiguration
    {
        string File { get; }
    }

    public class DockerConfiguration : IDockerConfiguration
    {
        private readonly ISpecFlowActionsConfiguration _specFlowActionsConfiguration;

        public DockerConfiguration(ISpecFlowActionsConfiguration specFlowActionsConfiguration)
        {
            _specFlowActionsConfiguration = specFlowActionsConfiguration;
        }

        public string File => _specFlowActionsConfiguration.Get("docker:file");
    }
}