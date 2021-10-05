using SpecFlow.Actions.WindowsAppDriver;
using SpecFlow.Actions.WindowsAppDriver.Configuration;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(WindowsAppDriverRuntimePlugin))]
namespace SpecFlow.Actions.WindowsAppDriver
{
    public class WindowsAppDriverRuntimePlugin : IRuntimePlugin
    {
        private IAppDriverCli _appDriverCli = null!;

        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.RegisterGlobalDependencies += RuntimePluginEvents_RegisterGlobalDependencies;
        }

        private void RuntimePluginEvents_RegisterGlobalDependencies(object sender, RegisterGlobalDependenciesEventArgs e)
        {
            var runtimePluginTestExecutionLifecycleEventEmitter = e.ObjectContainer.Resolve<RuntimePluginTestExecutionLifecycleEvents>();
            runtimePluginTestExecutionLifecycleEventEmitter.BeforeTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_BeforeTestRun;
            runtimePluginTestExecutionLifecycleEventEmitter.AfterTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun; ;

            e.ObjectContainer.RegisterTypeAs<WindowsAppDriverConfiguration, IWindowsAppDriverConfiguration>();
            e.ObjectContainer.RegisterTypeAs<AppDriverCli, IAppDriverCli>();
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_BeforeTestRun(object sender, RuntimePluginBeforeTestRunEventArgs e)
        {
            _appDriverCli = e.ObjectContainer.Resolve<AppDriverCli>();

            _appDriverCli.Start();
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun(object sender, RuntimePluginAfterTestRunEventArgs e)
        {
            _appDriverCli.Dispose();
        }
    }
}
