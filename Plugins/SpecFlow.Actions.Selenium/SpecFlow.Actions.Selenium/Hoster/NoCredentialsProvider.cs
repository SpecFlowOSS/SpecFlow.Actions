namespace SpecFlow.Actions.Selenium.Hoster;

public class NoCredentialsProvider : ICredentialProvider
{
    public string? Username => null;
    public string? UsernameArgumentName => null;
    public string? AccessKey => null;
    public string? AccessKeyArgumentName => null;
}