using BoDi;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Driver;
using System;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(SeleniumRuntimePlugin))]

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
            e.ObjectContainer.RegisterTypeAs<BrowserInteractions, IBrowserInteractions>();
            e.ObjectContainer.RegisterTypeAs<SeleniumConfiguration, ISeleniumConfiguration>();

            if (!e.ObjectContainer.IsRegistered<IDriverInitialiser>())
            {
                RegisterInitialisers(e.ObjectContainer);
            }
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitialiser>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();

                return config.Browser switch
                {
                    Browser.Chrome => new ChromeDriverInitialiser(config),
                    Browser.Firefox => new FirefoxDriverInitialiser(config),
                    Browser.Edge => new EdgeDriverInitialiser(config),
                    Browser.InternetExplorer => new InternetExplorerDriverInitialiser(config),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };
            });
        }
    }
}