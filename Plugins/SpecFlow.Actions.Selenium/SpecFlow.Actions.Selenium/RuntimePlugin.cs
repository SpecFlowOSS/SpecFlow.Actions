using SpecFlow.Actions.Selenium;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly:RuntimePlugin(typeof(SeleniumRuntimePlugin))]

namespace SpecFlow.Actions.Selenium
{
    
    public class SeleniumRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;  
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<SpecFlowJsonLoader, ISpecFlowJsonLoader>();
            e.ObjectContainer.RegisterTypeAs<SeleniumConfiguration, ISeleniumConfiguration>();

        }
    }
}