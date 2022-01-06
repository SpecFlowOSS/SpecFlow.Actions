using Selenium.Targets.Generation;
using SpecFlow.Actions.Configuration;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: GeneratorPlugin(typeof(TargetsGeneratorPlugin))]
namespace Selenium.Targets.Generation
{
    internal class TargetsGeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.CustomizeDependencies += GeneratorPluginEvents_CustomizeDependencies;
            generatorPluginEvents.RegisterDependencies += GeneratorPluginEvents_RegisterDependencies;
        }

        private void GeneratorPluginEvents_RegisterDependencies(object sender, RegisterDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLoader, ISpecFlowActionJsonLoader>();
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLocator, ISpecFlowActionJsonLocator>();
        }

        private void GeneratorPluginEvents_CustomizeDependencies(object sender, CustomizeDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLoader, ISpecFlowActionJsonLoader>();
            e.ObjectContainer.RegisterTypeAs<SpecFlowActionJsonLocator, ISpecFlowActionJsonLocator>();
            e.ObjectContainer.RegisterTypeAs<MultiFeatureGeneratorProvider, IFeatureGeneratorProvider>();
        }
    }
}