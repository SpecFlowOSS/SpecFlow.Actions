using BoDi;
using OpenQA.Selenium;
using SpecFlow.Actions.LambdaTest;
using SpecFlow.Actions.LambdaTest.DriverInitialisers;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(LambdaTestRuntimePlugin))]

namespace SpecFlow.Actions.LambdaTest
{
    public class LambdaTestRuntimePlugin : IRuntimePlugin
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
            runtimePluginTestExecutionLifecycleEventEmitter.AfterTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun;
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun(object sender, RuntimePluginAfterTestRunEventArgs e)
        {
            LambdaTestLocalService.Stop();
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario(object? sender, RuntimePluginAfterScenarioEventArgs e)
        {
            var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();
            var browserDriver = e.ObjectContainer.Resolve<BrowserDriver>();

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {
                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript("lambda-status=passed");
            }
            else
            {
                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript("lambda-status=failed");
            }
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<LambdaTestConfiguration, ISeleniumConfiguration>();
            RegisterInitialisers(e.ObjectContainer);
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitialiser>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();

                if (((LambdaTestConfiguration)config).BrowserstackLocalRequired)
                {
                    LambdaTestLocalService.Start(
                        ((LambdaTestConfiguration)config).BrowserstackLocalCapabilities.ToList());
                }

                var scenarioContext = container.Resolve<ScenarioContext>();

                return config.Browser switch
                {
                    Browser.Chrome => new LambdaTestChromeDriverInitialiser(config, scenarioContext),
                    Browser.Firefox => new LambdaTestFirefoxDriverInitialiser(config, scenarioContext),
                    Browser.Edge => new LambdaTestEdgeDriverInitialiser(config,scenarioContext),
                    Browser.InternetExplorer => new LambdaTestInternetExplorerDriverInitialiser(config, scenarioContext),
                    Browser.Safari => new LambdaTestSafariDriverInitialiser(config, scenarioContext),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };
            });
        }
    }
}