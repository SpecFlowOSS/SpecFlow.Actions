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
            e.ObjectContainer.RegisterTypeAs<OptionsConfigurator, IOptionsConfigurator>();

            RegisterInitialisers(e.ObjectContainer);
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitialiser>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();
                var optionsConfigurator = container.Resolve<IOptionsConfigurator>();

                switch (config.Browser)
                {
                    case Browser.Chrome:
                        return new ChromeDriverInitialiser(optionsConfigurator);

                    case Browser.Firefox:
                        return new FirefoxDriverInitialiser(optionsConfigurator);

                    case Browser.Edge:
                        return new EdgeDriverInitialiser(optionsConfigurator);

                    case Browser.InternetExplorer:
                        return new InternetExplorerDriverInitialiser(optionsConfigurator);

                    case Browser.None:
                    case Browser.Safari:
                    case Browser.Noop:
                    default:
                        throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented");
                }
            });
        }
    }
}