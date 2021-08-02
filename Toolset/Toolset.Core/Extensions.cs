using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Toolset.Core
{
    public static class Extensions
    {
        public static IToolsetBuilder AddToolset(this IServiceCollection services)
            => ToolsetBuilder.Create(services, ParamSource.JsonFile);

        public static TModel GetOptions<TModel, TCommandLine>(this IToolsetBuilder builder, string settingsSectionName, Func<TCommandLine, TModel> mapperFunc)
           where TModel : new()
           where TCommandLine : class
        {
            using var serviceProvider = builder.Services.BuildServiceProvider();

            if (builder.ParamSource == ParamSource.CommandLine)
            {
                var commandLineParams = serviceProvider.GetService<ICommandLineData>();
                return mapperFunc(commandLineParams.Get<TCommandLine>());
            }

            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetOptions<TModel>(settingsSectionName);
        }

        public static TModel GetOptions<TModel>(this IToolsetBuilder builder, string settingsSectionName)
            where TModel : new()
        {
            using var serviceProvider = builder.Services.BuildServiceProvider();

            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetOptions<TModel>(settingsSectionName);
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }
    }
}
