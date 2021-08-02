using System;
using System.Net.Http;
using System.Threading.Tasks;
using Toolsed.Shared;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Create
{
    internal sealed class CreateAssetDirectory : BaseProGetAsset, ICreateAssetDirectory
    {
        private readonly IProGetHomeHttpClient _proGetHomeHttpClient;
        
        public CreateAssetDirectory(ProGetHomeOptions proGetHomeOptions, IProGetHomeHttpClient proGetHomeHttpClient)
            : base(proGetHomeOptions, $"endpoints/{proGetHomeOptions.AssetName.AppendIfNotPresent('/')}dir/")
            => _proGetHomeHttpClient = proGetHomeHttpClient;

        public async Task<HttpResponseMessage> Create(string directory)
        {
            if(!directory.HasValue()) { throw new ArgumentException(null, nameof(directory)); }

            return await _proGetHomeHttpClient.PostAsync($"{ProGetUrl.AppendIfNotPresent('/')}{directory}").ConfigureAwait(false);
        }
    }
}
