using SpecFlow.Actions.Appium;
using SpecFlow.Actions.Appium.Configuration;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(AppiumRuntimePlugin))]

namespace SpecFlow.Actions.Appium
{
    public class AppiumRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<AppiumConfiguration, IAppiumConfiguration>();
            e.ObjectContainer.RegisterTypeAs<AppiumServer, IAppiumServer>();
            e.ObjectContainer.RegisterTypeAs<DriverOptions, IDriverOptions>();
            e.ObjectContainer.RegisterTypeAs<AppDriver, IAppDriver>();

            var configuration = e.ObjectContainer.Resolve<AppiumConfiguration>();
            var server = e.ObjectContainer.Resolve<AppiumServer>();

            if (configuration.LocalAppiumServerRequired)
            {
                server.Current.Start();
            }
        }
    }
}