using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Configuration.Generation;
using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: GeneratorPlugin(typeof(TargetsGeneratorPlugin))]
namespace SpecFlow.Actions.Configuration.Generation
{
    internal class TargetsGeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.CustomizeDependencies += GeneratorPluginEvents_CustomizeDependencies;
        }

        private void GeneratorPluginEvents_CustomizeDependencies(object sender, CustomizeDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLoader, ISpecFlowActionJsonLoader>();
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLocator, ISpecFlowActionJsonLocator>();
            e.ObjectContainer.RegisterTypeAs<MultiFeatureGeneratorProvider, IFeatureGeneratorProvider>();
            e.ObjectContainer.RegisterTypeAs<TargetIdentifier, ITargetIdentifier>();
        }
    }
}