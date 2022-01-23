using BoDi;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Driver;
using SpecFlow.Actions.Selenium.DriverOptions;
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
            e.ObjectContainer.RegisterTypeAs<SeleniumConfiguration, ISeleniumConfiguration>();
            e.ObjectContainer.RegisterTypeAs<BrowserInteractions, IBrowserInteractions>();

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
                IOptionsConfigurator optionsConfigurator = new OptionsConfigurator(config);

                return config.Browser switch
                {
                    Browser.Chrome => new ChromeDriverInitialiser(optionsConfigurator),
                    Browser.Firefox => new FirefoxDriverInitialiser(optionsConfigurator),
                    Browser.Edge => new EdgeDriverInitialiser(optionsConfigurator),
                    Browser.InternetExplorer => new InternetExplorerDriverInitialiser(optionsConfigurator),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };
            });
        }
    }
}