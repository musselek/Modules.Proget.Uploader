using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net.Http;
using Toolset.Core;
using Toolset.Http.ProGetHome.AssetDirectory.Create;
using Toolset.Http.ProGetHome.AssetDirectory.Delete;
using Toolset.Http.ProGetHome.AssetDirectory.Get;
using Toolset.Http.ProGetHome.AssetDirectory.Upload;
using Toolset.Http.ProGetHome.CommandLineContract;
using Toolset.Http.ProGetHome.HttpClientService;
using Toolset.Http.ProGetHome.Mappers;

namespace Toolset.Http.ProGetHome
{
    public static class Extensions
    {
        public static IToolsetBuilder AddProGetHome(this IToolsetBuilder builder
            , string sectionName = "ProGetHome"
            , Action<DelegateResult<HttpResponseMessage>, TimeSpan> onRetry = null
            )
        {
            var proGetHomeOptions = builder.GetOptions<ProGetHomeOptions, IProGetHomeCommandLineArgs>(sectionName, MapBasedOnCommandLine.Map);
            
            builder.Services.AddTransient(sp => onRetry);
            builder.Services.AddSingleton(proGetHomeOptions);
            builder.Services.AddHttpClient<IProGetHomeHttpClient, ProGetHomeHttpClient>("toolset");
            builder.Services.AddTransient(typeof(IDeleteAsset), typeof(DeleteAsset));
            builder.Services.AddTransient(typeof(IUploadProGetAsset), typeof(UploadProGetAsset));
            builder.Services.AddTransient(typeof(ICreateAssetDirectory), typeof(CreateAssetDirectory));
            builder.Services.AddTransient(typeof(IGetAssetEndpoint), typeof(GetAssetEndpoint));
            builder.Services.AddTransient(typeof(IChunkFileUploader), typeof(ChunkFileUploader));
            builder.Services.AddTransient(typeof(IWholeFileUploader), typeof(WholeFileUploader));
            builder.Services.AddTransient(typeof(IChunkProcessor), typeof(ChunkProcessor));
            builder.Services.AddTransient(typeof(IDownloadAssetEndpoint), typeof(DownloadAssetEndpoint));

            return builder;
        }
    }
}
