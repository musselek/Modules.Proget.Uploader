namespace Toolset.Http.ProGetHome.CommandLineContract
{
    public interface IProGetHomeCommandLineArgs
    {
        string ApiKey { get; set; }
        string AssetName { get; set; }
        int ChunkSize { get; set; }
        string EndPointAddress { get; set; }
    }
}
