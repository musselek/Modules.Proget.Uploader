using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Upload
{
    internal sealed class WholeFileUploader : IWholeFileUploader
    {
        private readonly IProGetHomeHttpClient _proGetHomeHttpClient;
        public WholeFileUploader(IProGetHomeHttpClient proGetHomeHttpClient) => _proGetHomeHttpClient = proGetHomeHttpClient;

        public async Task<HttpResponseMessage> Upload(FileStream fileStream, string endpointUrl)
        {
            using var httpContent = new StreamContent(fileStream);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpContent.Headers.ContentLength = fileStream.Length;

            return await _proGetHomeHttpClient.PostAsync($"{endpointUrl}", httpContent).ConfigureAwait(false);
        }
    }
}
