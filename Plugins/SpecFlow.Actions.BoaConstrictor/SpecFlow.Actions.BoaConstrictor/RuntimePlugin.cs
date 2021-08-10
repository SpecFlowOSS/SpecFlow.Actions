using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using SpecFlow.Actions.BoaConstrictor;
using SpecFlow.Actions.Selenium;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly:RuntimePlugin(typeof(BoaConstrictorRuntimePlugin))]

namespace SpecFlow.Actions.BoaConstrictor
{
    public class BoaConstrictorRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;            
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterFactoryAs<Actor>((container) =>
            {
                var specFlowLogger = e.ObjectContainer.Resolve<SpecFlowLogger>();
                var browserDriver = e.ObjectContainer.Resolve<BrowserDriver>();

                var actor = new Actor(logger: specFlowLogger);
                actor.Can(BrowseTheWeb.With(browserDriver.Current));

                return actor;
            });

            e.ObjectContainer.RegisterFactoryAs<IActor>((container) => container.Resolve<Actor>());
        }
    }
}