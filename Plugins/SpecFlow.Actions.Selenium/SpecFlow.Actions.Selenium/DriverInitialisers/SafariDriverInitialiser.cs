using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Hoster;
using System;

namespace SpecFlow.Actions.Selenium.DriverInitialisers;

public class SafariDriverInitialiser : DriverInitialiser<SafariOptions>
{
    private static readonly Lazy<string?> SafariWebDriverFilePath =
        new Lazy<string?>(() => Environment.GetEnvironmentVariable("SAFARI_WEBDRIVER_FILE_PATH"));

    public SafariDriverInitialiser(ISeleniumConfiguration seleniumConfiguration, ICredentialProvider credentialProvider)
        : base(seleniumConfiguration, credentialProvider)
    {
    }

    protected override void AddDefaultCapabilities(SafariOptions options)
    {
        
    }

    protected override IWebDriver CreateWebDriver(SafariOptions options)
    {
        return string.IsNullOrWhiteSpace(SafariWebDriverFilePath.Value)
            ? new SafariDriver(SafariDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(120))
            : new SafariDriver(SafariDriverService.CreateDefaultService(SafariWebDriverFilePath.Value), options,
                TimeSpan.FromSeconds(120));
    }
}