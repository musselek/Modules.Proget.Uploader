using Microsoft.Extensions.DependencyInjection;
using Toolset.Core;

namespace Toolset.Http
{
    public static class Extensions
    {
        public static IToolsetBuilder AddHttpClient(this IToolsetBuilder builder
            , string httpClientSectionName = "httpClient")
        {
            var httpClientOptions = builder.GetOptions<HttpClientOptions, IHttpCommandLineArgs>(httpClientSectionName, MapBasedOnCommandLine.Map);

            builder.Services.AddSingleton(httpClientOptions);
            builder.Services.AddHttpClient<IToolsetHttpClient, ToolsetHttpClient>("toolset");
            return builder;
        }
    }
}
