using SpecFlow.Actions.Selenium.Hoster;
using System;

namespace SpecFlow.Actions.LambdaTest;

public class LambdaTestCredentialProvider : ICredentialProvider
{
    private readonly Lazy<string?> _username;
    private readonly Lazy<string?> _accessKey;


    public LambdaTestCredentialProvider()
    {
        _username = new Lazy<string?>(() => Environment.GetEnvironmentVariable("LT_USERNAME"));
        _accessKey = new Lazy<string?>(() => Environment.GetEnvironmentVariable("LT_ACCESS_KEY"));
    }

    public string? Username => _username.Value;
    public string? UsernameArgumentName => "user";
    public string? AccessKey => _accessKey.Value;
    public string? AccessKeyArgumentName => "accessKey";
}