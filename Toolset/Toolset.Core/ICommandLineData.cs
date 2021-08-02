namespace Toolset.Core
{
    public interface ICommandLineData
    {
        T Get<T>() where T : class;
    }
}
