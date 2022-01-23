using BoDi;
using OpenQA.Selenium;
using Specflow.Actions.Browserstack;
using SpecFlow.Actions.Browserstack;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Driver;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(BrowserstackRuntimePlugin))]

namespace Specflow.Actions.Browserstack
{
    public class BrowserstackRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
            runtimePluginEvents.CustomizeGlobalDependencies += RuntimePluginEvents_CustomizeGlobalDependencies;
        }

        private void RuntimePluginEvents_CustomizeGlobalDependencies(object? sender, CustomizeGlobalDependenciesEventArgs e)
        {
            var runtimePluginTestExecutionLifecycleEventEmitter = e.ObjectContainer.Resolve<RuntimePluginTestExecutionLifecycleEvents>();
            runtimePluginTestExecutionLifecycleEventEmitter.AfterScenario += RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario;
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario(object? sender, RuntimePluginAfterScenarioEventArgs e)
        {
            var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();
            var browserDriver = e.ObjectContainer.Resolve<BrowserDriver>();

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {

                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript(BrowserstackTestResultExecutor.GetResultExecutor("passed"));
            }
            else
            {
                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript(BrowserstackTestResultExecutor.GetResultExecutor("failed", scenarioContext.TestError.Message));
            }
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<BrowserstackLocalService, IBrowserstackLocalService>();

            RegisterInitialisers(e.ObjectContainer);
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitialiser>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();
                var scenarioContext = container.Resolve<ScenarioContext>();
                IOptionsConfigurator optionsConfigurator = new BrowserstackOptionsConfigurator(config, scenarioContext);

                IDriverOptions options = config.Browser switch
                {
                    Browser.Chrome => new ChromeDriverOptions(),
                    Browser.Firefox => new FirefoxDriverOptions(),
                    Browser.Edge => new EdgeDriverOptions(),
                    Browser.InternetExplorer => new InternetExplorerDriverOptions(),
                    Browser.Safari => new SafariDriverOptions(),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };

                return new BrowserstackDriverInitialiser(optionsConfigurator, options);
            });
        }
    }
}