using CommandLine;
using System.Collections.Generic;
using Toolset.Http;
using Toolset.Http.ProGetHome.CommandLineContract;

namespace Modules.Proget.Uploader.CommandLine
{
    public sealed class CommandLineArgs : IProGetHomeCommandLineArgs, IHttpCommandLineArgs
    {
        [Option('s', "sourceFolder", Required = false, HelpText = "Specify the source folder")]
        public string SourceFolder { get; set; }

        [Option('f', "files", HelpText = "Specify files separate by whitespace ")]
        public IEnumerable<string> Files { get; set; }

        [Option('d', "destinationFolder", Required = true, HelpText = "Specify the destination folder")]
        public string DestinationFolder { get; set; }

        [Option('c', "chunkSize", Required = false, HelpText = "Specify the chunk size in bytes", Default = 1024 * 1024 * 5)]
        public int ChunkSize { get; set; }

        [Option('a', "apiKey", Required = false, HelpText = "Specify the api key")]
        public string ApiKey { get; set; }

        [Option('r', "retries", Required = false, HelpText = "Specify how many times to retry  in case of upload fail", Default = 3)]
        public int Retries { get; set; }

        [Option('n', "assetName", Required = false, HelpText = "Specify the asset name")]
        public string AssetName { get; set; }

        [Option('e', "endPoint", Required = false, HelpText = "Specify the end point address")]
        public string EndPointAddress { get; set; }

        [Option('t', "timeout", Required = false, HelpText = "Specify the request timeout in miliseconds", Default = 30000)]
        public int TimeoutInMs { get; set; }
    }
}
