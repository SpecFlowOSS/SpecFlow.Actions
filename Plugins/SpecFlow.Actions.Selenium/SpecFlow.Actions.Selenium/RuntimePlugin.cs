using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverOptions;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(SeleniumRuntimePlugin))]

namespace SpecFlow.Actions.Selenium
{
    public class SeleniumRuntimePlugin : IRuntimePlugin
    {
        private const string Local = "local";

        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;  
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<SeleniumConfiguration, ISeleniumConfiguration>();
            e.ObjectContainer.RegisterTypeAs<DriverInitialiser, IDriverInitialiser>(Local);
            e.ObjectContainer.RegisterTypeAs<BrowserInteractions, IBrowserInteractions>();
            e.ObjectContainer.RegisterTypeAs<WebDriverOptions, IWebDriverOptions>();
            e.ObjectContainer.RegisterTypeAs<LocalOptionsConfigurator, IOptionsConfigurator>(Local);
            e.ObjectContainer.RegisterTypeAs<DriverFactory, IDriverFactory>();
        }
    }
}