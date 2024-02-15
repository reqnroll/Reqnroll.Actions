namespace Reqnroll.Actions.Configuration
{
    public interface IReqnrollActionJsonLocator
    {
        string? GetFilePath();
        string? GetTargetFilePath(string targetName);
    }
}