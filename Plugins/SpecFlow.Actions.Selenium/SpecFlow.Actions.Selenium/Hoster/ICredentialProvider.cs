namespace SpecFlow.Actions.Selenium.Hoster;

public interface ICredentialProvider
{
    string? Username { get; }
    string? UsernameArgumentName { get; }
    string? AccessKey { get; }
    string? AccessKeyArgumentName { get; }
}