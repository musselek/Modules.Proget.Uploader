using System;
using Toolset.Http.ProGetHome.CommandLineContract;

namespace Toolset.Http.ProGetHome.Mappers
{
    static internal class MapBasedOnCommandLine
    {
        public static ProGetHomeOptions Map(IProGetHomeCommandLineArgs commandLineArgs)
        {
            if (commandLineArgs is null) { throw new ArgumentNullException(nameof(commandLineArgs)); }

            return new()
            {
                ApiKey = commandLineArgs.ApiKey,
                ChunkSize = Math.Abs(commandLineArgs.ChunkSize),
                AssetName = commandLineArgs.AssetName,
                EndpointAddress = commandLineArgs.EndPointAddress
            };
        }
    }
}
