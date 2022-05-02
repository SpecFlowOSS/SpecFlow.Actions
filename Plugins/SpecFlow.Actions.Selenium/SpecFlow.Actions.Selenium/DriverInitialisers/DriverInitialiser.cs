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

        if (_seleniumConfiguration.Capabilities.Any())
        {
            foreach (var capability in _seleniumConfiguration.Capabilities)
            {
                options.TryToAddGlobalCapability(capability.Key, capability.Value);
            }
        }

        if (_credentialProvider.UsernameArgumentName is not null && _credentialProvider.Username is not null)
        {
            options.TryToAddGlobalCapability(_credentialProvider.UsernameArgumentName,  _credentialProvider.Username);
        }

        if (_credentialProvider.AccessKeyArgumentName is not null && _credentialProvider.AccessKey is not null)
        {
            options.TryToAddGlobalCapability(_credentialProvider.AccessKeyArgumentName, _credentialProvider.AccessKey);
        }

        if (_seleniumConfiguration.Arguments.Any())
        {
            options.TryToAddArguments(_seleniumConfiguration.Arguments);
        }

        return options;
    }


    protected abstract IWebDriver CreateWebDriver(T options);
}