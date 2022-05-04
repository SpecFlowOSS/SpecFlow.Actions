using SpecFlow.Actions.Selenium.Hoster;
using System;

namespace Specflow.Actions.Browserstack;

public class BrowserstackCredentialProvider : ICredentialProvider
{
    private readonly Lazy<string?> _browserstackUsername;
    private readonly Lazy<string?> _accessKey;
        

    public BrowserstackCredentialProvider()
    {
        _browserstackUsername = new Lazy<string?>(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        _accessKey = new Lazy<string?>(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));
    }

    public string? Username => _browserstackUsername.Value;
    public string? UsernameArgumentName => "browserstack.user";
    public string? AccessKey => _accessKey.Value;
    public string? AccessKeyArgumentName => "browserstack.key";
}