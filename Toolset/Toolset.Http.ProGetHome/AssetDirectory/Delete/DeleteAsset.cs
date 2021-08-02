using System;
using System.Net.Http;
using System.Threading.Tasks;
using Toolsed.Shared;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Delete
{
    internal sealed class DeleteAsset : BaseProGetAsset, IDeleteAsset
    {
        private readonly IProGetHomeHttpClient _proGetHomeHttpClient;

        public DeleteAsset(ProGetHomeOptions proGetHomeOptions, IProGetHomeHttpClient proGetHomeHttpClient)
            : base(proGetHomeOptions, $"endpoints/{proGetHomeOptions.AssetName.AppendIfNotPresent('/')}content/")
            => _proGetHomeHttpClient = proGetHomeHttpClient;

        public async Task<HttpResponseMessage> Delete(string assetRootPath)
        {
            if (!assetRootPath.HasValue()) { throw new ArgumentNullException(nameof(assetRootPath)); }

            return await _proGetHomeHttpClient.DeleteAsync($"{ProGetUrl.AppendIfNotPresent('/')}{assetRootPath}").ConfigureAwait(false);
        }
    }
}
