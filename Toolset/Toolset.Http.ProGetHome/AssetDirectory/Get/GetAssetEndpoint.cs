using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolsed.Shared;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Get
{
    internal sealed class GetAssetEndpoint : BaseProGetAsset, IGetAssetEndpoint
    {
        private readonly IProGetHomeHttpClient _proGetHomeHttpClient;

        public GetAssetEndpoint(ProGetHomeOptions proGetHomeOptions, IProGetHomeHttpClient proGetHomeHttpClient) 
            : base(proGetHomeOptions, $"endpoints/{proGetHomeOptions.AssetName.AppendIfNotPresent('/')}dir/")
            => _proGetHomeHttpClient = proGetHomeHttpClient;

        public async Task<IEnumerable<AssetDirectoryItem>> Get(string directory, bool recursive)
        {
            if (!directory.HasValue()) { throw new ArgumentException(null, nameof(directory)); }

            return await _proGetHomeHttpClient.GetAsync<IEnumerable<AssetDirectoryItem>>($"{ProGetUrl.AppendIfNotPresent('/')}{directory}?recursive={recursive}").ConfigureAwait(false)
                ?? Enumerable.Empty<AssetDirectoryItem>();
        }
    }
}
