using SpecFlow.Actions.Android;
using SpecFlow.Actions.Android.Configuration;
using SpecFlow.Actions.Android.Driver;
using SpecFlow.Actions.Android.Server;
using SpecFlow.Actions.Appium.Configuration.Android;
using SpecFlow.Actions.Appium.Driver;
using SpecFlow.Actions.Appium.Server;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(AndroidRuntimePlugin))]
namespace SpecFlow.Actions.Android
{
    class AndroidRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<AndroidConfiguration, IAndroidConfiguration>();
            e.ObjectContainer.RegisterTypeAs<AppiumServer, IAppiumServer>();
            e.ObjectContainer.RegisterTypeAs<AndroidAppDriverOptions, IDriverOptions>();

            var configuration = e.ObjectContainer.Resolve<AndroidConfiguration>();
            var server = e.ObjectContainer.Resolve<AppiumServer>();

            if (configuration.LocalAppiumServerRequired)
            {
                server.Current.Start();
            }
        }
    }
}