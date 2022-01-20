using BoDi;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Driver;
using SpecFlow.Actions.Selenium.DriverOptions;
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
            e.ObjectContainer.RegisterTypeAs<WebDriverOptionsFactory, IWebDriverOptionsFactory>();
            
            RegisterInitialisers(e.ObjectContainer);
            RegisterLocalObjects(e.ObjectContainer);
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterTypeAs<ChromeDriverInitialiser, IDriverInitialiser>(Browser.Chrome.ToString());
            objectContainer.RegisterTypeAs<EdgeDriverInitialiser, IDriverInitialiser>(Browser.Edge.ToString());
            objectContainer.RegisterTypeAs<FirefoxDriverInitialiser, IDriverInitialiser>(Browser.Firefox.ToString());
            objectContainer.RegisterTypeAs<InternetExplorerDriverInitialiser, IDriverInitialiser>(Browser.InternetExplorer.ToString());
            objectContainer.RegisterTypeAs<SafariDriverInitialiser, IDriverInitialiser>(Browser.Safari.ToString());

            objectContainer.RegisterFactoryAs(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();

                return new ChromeDriverLocalInitialiser();
            });


        }

        private void RegisterLocalObjects(IObjectContainer objectContainer)
        {
            var config = objectContainer.Resolve<ISeleniumConfiguration>();

            if (config.TestPlatform.Equals("local"))
            {
                objectContainer.RegisterTypeAs<LocalDriverFactory, IDriverFactory>();
                objectContainer.RegisterTypeAs<LocalOptionsConfigurator, IOptionsConfigurator>();
            }
        }
    }
}