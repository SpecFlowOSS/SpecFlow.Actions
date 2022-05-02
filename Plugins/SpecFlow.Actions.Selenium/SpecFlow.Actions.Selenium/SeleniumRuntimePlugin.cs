using BoDi;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.DriverInitialisers;
using SpecFlow.Actions.Selenium.Hoster;
using System;
using System.Diagnostics.Contracts;
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
            if (!e.ObjectContainer.IsRegistered<ISeleniumConfiguration>())
            {
                e.ObjectContainer.RegisterTypeAs<SeleniumConfiguration, ISeleniumConfiguration>(); 
            }

            if (!e.ObjectContainer.IsRegistered<IDriverInitialiser>())
            {
                RegisterInitialisers(e.ObjectContainer);
            }

            if (!e.ObjectContainer.IsRegistered<ICredentialProvider>())
            {
                e.ObjectContainer.RegisterTypeAs<NoCredentialsProvider, ICredentialProvider>();
            }

            e.ObjectContainer.RegisterTypeAs<BrowserInteractions, IBrowserInteractions>();
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitialiser>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();

                var credentialProvider = container.Resolve<ICredentialProvider>();
                return config.Browser switch
                {
                    Browser.Chrome => new ChromeDriverInitialiser(config, credentialProvider),
                    Browser.Firefox => new FirefoxDriverInitialiser(config, credentialProvider),
                    Browser.Edge => new EdgeDriverInitialiser(config, credentialProvider),
                    Browser.InternetExplorer => new InternetExplorerDriverInitialiser(config, credentialProvider),
                    Browser.Safari => new SafariDriverInitialiser(config, credentialProvider),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };
            });
        }
    }
}