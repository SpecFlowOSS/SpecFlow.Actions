using SpecFlow.Actions.Docker;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly:RuntimePlugin(typeof(DockerRuntimePlugin))]


namespace SpecFlow.Actions.Docker
{
    public class DockerRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<DockerHandling, IDockerHandling>();
            e.ObjectContainer.RegisterTypeAs<DockerConfiguration, IDockerConfiguration>();

            var specFlowConfiguration = e.ObjectContainer.Resolve<SpecFlowConfiguration>();
            specFlowConfiguration.AdditionalStepAssemblies.Add("SpecFlow.Actions.Docker.SpecFlowPlugin");
        }
    }
}