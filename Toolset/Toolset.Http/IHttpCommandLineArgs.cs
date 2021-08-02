namespace Toolset.Http
{
    public interface IHttpCommandLineArgs
    {
        int Retries { get; set; }
        int TimeoutInMs { get; set; }
    }
}
