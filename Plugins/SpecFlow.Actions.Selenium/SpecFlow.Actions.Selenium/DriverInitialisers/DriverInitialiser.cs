using OpenQA.Selenium;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Hoster;
using System.Linq;

namespace SpecFlow.Actions.Selenium.DriverInitialisers;

public abstract class DriverInitialiser<T> : IDriverInitialiser where T : DriverOptions, new()
{
    private readonly ISeleniumConfiguration _seleniumConfiguration;
    private readonly ICredentialProvider _credentialProvider;

    protected DriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider)
    {
        _seleniumConfiguration = seleniumConfiguration;
        _credentialProvider = credentialProvider;
    }

    public IWebDriver Initialise()
    {
        var option = CreateOptions();

        return CreateWebDriver(option);
    }

    private T CreateOptions()
    {
        var options = new T();

        AddDefaultCapabilities(options);

        AddCapabilitiesFromConfiguration(options);

        AddCredentialCapabilities(options);

        AddArgumentsFromConfiguration(options);

        return options;
    }

    protected abstract void AddDefaultCapabilities(T options);

    private void AddArgumentsFromConfiguration(T options)
    {
        if (_seleniumConfiguration.Arguments.Any())
        {
            options.TryToAddArguments(_seleniumConfiguration.Arguments);
        }
    }

    private void AddCredentialCapabilities(T options)
    {
        if (_credentialProvider.UsernameArgumentName is not null && _credentialProvider.Username is not null)
        {
            options.TryToAddGlobalCapability(_credentialProvider.UsernameArgumentName, _credentialProvider.Username);
        }

        if (_credentialProvider.AccessKeyArgumentName is not null && _credentialProvider.AccessKey is not null)
        {
            options.TryToAddGlobalCapability(_credentialProvider.AccessKeyArgumentName, _credentialProvider.AccessKey);
        }
    }

    private void AddCapabilitiesFromConfiguration(T options)
    {
        if (_seleniumConfiguration.Capabilities.Any())
        {
            foreach (var capability in _seleniumConfiguration.Capabilities)
            {
                options.TryToAddGlobalCapability(capability.Key, capability.Value);
            }
        }
    }


    protected abstract IWebDriver CreateWebDriver(T options);
}