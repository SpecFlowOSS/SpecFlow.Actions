using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
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
            e.ObjectContainer.RegisterTypeAs<DriverFactory, IDriverFactory>();
            e.ObjectContainer.RegisterTypeAs<WebDriverOptionsFactory, IWebDriverOptionsFactory>();
            e.ObjectContainer.RegisterTypeAs<LocalOptionsConfigurator, IOptionsConfigurator>();
            e.ObjectContainer.RegisterTypeAs<ChromeDriverInitialiser, IDriverInitialiser>(Browser.Chrome.ToString());
            e.ObjectContainer.RegisterTypeAs<EdgeDriverInitialiser, IDriverInitialiser>(Browser.Edge.ToString());
            e.ObjectContainer.RegisterTypeAs<FirefoxDriverInitialiser, IDriverInitialiser>(Browser.Firefox.ToString());
            e.ObjectContainer.RegisterTypeAs<InternetExplorerDriverInitialiser, IDriverInitialiser>(Browser.InternetExplorer.ToString());
        }
    }
}