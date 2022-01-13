namespace SpecFlow.Actions.Configuration
{
    public interface ISpecFlowActionJsonLocator
    {
        string? GetFilePath();
        string? GetTargetFilePath(string targetName);
    }
}