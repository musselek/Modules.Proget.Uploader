using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Toolset.Core;

namespace Toolset.CommandLine
{
    public static class Extensions
    {
        public static IToolsetBuilder AddCommandLineParser<T>(this IToolsetBuilder builder, string[] args, Action<IEnumerable<Error>> action, bool commandLineAsParamSource = false) where T : class
        {
            Parser.Default
                .ParseArguments<T>(args)
                .WithNotParsed(errors => action(errors))
                .WithParsed(opts => builder.Services.AddSingleton(typeof(ICommandLineData), new CommandLineData(opts)));

            builder.SetParamSource(commandLineAsParamSource
                                        ? ParamSource.CommandLine
                                        : ParamSource.JsonFile
                                        );

            return builder;
        }
    }
}
