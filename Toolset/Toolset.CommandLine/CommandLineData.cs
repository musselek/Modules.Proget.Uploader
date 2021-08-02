using Toolset.Core;

namespace Toolset.CommandLine
{
    internal sealed class CommandLineData : ICommandLineData
    {
        private readonly object _commandLineData;

        public CommandLineData(object commandLineData)
            => _commandLineData = commandLineData;

        public T Get<T>() where T : class
            => _commandLineData as T;
    }
}
