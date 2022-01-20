using BoDi;
using OpenQA.Selenium;
using Specflow.Actions.Browserstack;
using SpecFlow.Actions.Browserstack;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Driver;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using System.Diagnostics;
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
            e.ObjectContainer.RegisterTypeAs<BrowserstackConfiguration, ISeleniumConfiguration>();

            RegisterBrowserstackObjects(e.ObjectContainer);
        }



        private void RegisterBrowserstackObjects(IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<ChromeDriverLocalInitialiser_, IDriverInitialiser_>(Browser.Chrome.ToString());
            //objectContainer.RegisterTypeAs<FirefoxDriverLocalInitialiser_, IDriverInitialiser_>(Browser.Edge.ToString());

            //var instance = objectContainer.Resolve<IDriverInitialiser_>(Browser.Chrome.ToString());
           // instance.Dump();

            objectContainer.RegisterFactoryAs<IWebDriver>(container =>
            {
                var config = container.Resolve<cfg>();

                switch (config.BrowserType)
                {
                    case "Chrome":
                        var options = _driverOptionsFactory.GetChromeOptions();
                        return new ChromeDriverLocalInitialiser_(config, options);
                    default:
                        return new FirefoxDriverLocalInitialiser_();
                }
            });


            var cfg = new cfg{BrowserType = "Firefox"};
            objectContainer.RegisterInstanceAs(cfg);

            var instance_ = objectContainer.Resolve<Consumer>();

            cfg.BrowserType = "Chrome";

            instance_ = objectContainer.Resolve<Consumer>();


            var config = objectContainer.Resolve<ISeleniumConfiguration>();

            if (config.TestPlatform.Equals("browserstack"))
            {
                objectContainer.RegisterTypeAs<BrowserstackDriverFactory, IDriverFactory>();
                objectContainer.RegisterTypeAs<BrowserstackOptionsConfigurator, IOptionsConfigurator>();
            }
        }
    }

    class Consumer
    {
        private IDriverInitialiser_ initialiser;

        public Consumer(IDriverInitialiser_ initialiser, cfg cfg)
        {
            this.initialiser = initialiser;
        }
    }


    [DebuggerDisplay("{BrowserType}")]
    class cfg
    {
        public string BrowserType { get; set; } = "Chrome";
    }

    interface IDriverInitialiser_
    {
        void Dump();
    }

    class ChromeDriverLocalInitialiser_ : IDriverInitialiser_
    {
        private cfg _cfg;

        public ChromeDriverLocalInitialiser_(cfg cfg)
        {
            _cfg = cfg;
        }

        public void Dump()
        {
            throw new Exception($"I am {nameof(ChromeDriverLocalInitialiser_)}");
        }
    }

    class FirefoxDriverLocalInitialiser_ : IDriverInitialiser_
    {
        public void Dump()
        {
            throw new Exception($"I am {nameof(FirefoxDriverLocalInitialiser_)}");
        }
    }
}

