using System;
using System.Threading.Tasks;
using Toolsed.Shared;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Get
{
    internal sealed class DownloadAssetEndpoint : BaseProGetAsset, IDownloadAssetEndpoint
    {
        private readonly IProGetHomeHttpClient _proGetHomeHttpClient;

        public DownloadAssetEndpoint(ProGetHomeOptions proGetHomeOptions, IProGetHomeHttpClient proGetHomeHttpClient) 
            : base(proGetHomeOptions, $"endpoints/{proGetHomeOptions.AssetName.AppendIfNotPresent('/')}content/")
            => _proGetHomeHttpClient = proGetHomeHttpClient;

        public async Task<byte[]> Downloald(string assetEndPoint)
        {
            if (!assetEndPoint.HasValue()) { throw new ArgumentException(null, nameof(assetEndPoint)); }

            return await _proGetHomeHttpClient.GetAsync<byte[]>($"{ProGetUrl.AppendIfNotPresent('/')}{assetEndPoint}").ConfigureAwait(false)
                ?? Array.Empty<byte>();
        }
    }
}
