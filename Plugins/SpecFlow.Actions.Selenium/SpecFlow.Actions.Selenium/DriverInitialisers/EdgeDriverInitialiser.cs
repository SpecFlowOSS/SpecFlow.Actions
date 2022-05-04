using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Hoster;
using System;

namespace SpecFlow.Actions.Selenium.DriverInitialisers;

public class EdgeDriverInitialiser : DriverInitialiser<EdgeOptions>
{
    private static readonly Lazy<string?> EdgeWebDriverFilePath =
        new Lazy<string?>(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));

    public EdgeDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider) :
        base(seleniumConfiguration, credentialProvider)
    {
    }


    protected override IWebDriver CreateWebDriver(EdgeOptions options)
    {
        return string.IsNullOrWhiteSpace(EdgeWebDriverFilePath.Value)
            ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
            : new EdgeDriver(EdgeDriverService.CreateDefaultService(EdgeWebDriverFilePath.Value), options,
                TimeSpan.FromSeconds(120));
    }
}