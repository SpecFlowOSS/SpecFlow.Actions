using SpecFlow.Actions.WindowsAppDriver;
using SpecFlow.Actions.WindowsAppDriver.Configuration;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(WindowsAppDriverRuntimePlugin))]
namespace SpecFlow.Actions.WindowsAppDriver
{
    class WindowsAppDriverRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<WindowsAppDriverConfiguration, IWindowsAppDriverConfiguration>();
        }
    }
}
