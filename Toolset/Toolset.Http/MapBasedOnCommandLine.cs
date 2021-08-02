using System;

namespace Toolset.Http
{
    static internal class MapBasedOnCommandLine
    {
        public static HttpClientOptions Map(IHttpCommandLineArgs commandLineArgs)
        {
            if(commandLineArgs is null) { throw new ArgumentNullException(nameof(commandLineArgs)); }

            return new()
            {
                Retries = commandLineArgs.Retries,
                TimeoutInMs = commandLineArgs.TimeoutInMs
            };
        }
    }
}
