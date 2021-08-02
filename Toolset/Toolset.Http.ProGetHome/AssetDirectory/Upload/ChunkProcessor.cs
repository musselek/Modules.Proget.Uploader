using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Upload
{
    internal class ChunkProcessor : IChunkProcessor
    {
        private Guid _guid;

        public ChunkProcessor()
            => _guid = Guid.NewGuid();

        public async Task<HttpResponseMessage> OnComplete(
            string endpointUrl
            , IProGetHomeHttpClient proGetHomeHttp)
        {
            using var httpContent = new StreamContent(new MemoryStream());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpContent.Headers.ContentLength = 0;

            return await proGetHomeHttp.PostAsync($"{endpointUrl}?multipart=complete&id={_guid}", httpContent).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> OnUpload(byte[] buffer
            , string endpointUrl
            , int index
            , int offset
            , long fileLength
            , long currentChunkSize
            , long totalParts
            , IProGetHomeHttpClient proGetHomeHttp)
        {
            using var stream = new MemoryStream(buffer);
            using var httpContent = new StreamContent(stream);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpContent.Headers.ContentLength = currentChunkSize;

            var multiPart = $"multipart=upload&id={_guid}&index={index}&offset={offset}&totalSize={fileLength}&partSize={currentChunkSize}&totalParts={totalParts}";

            return await proGetHomeHttp.PostAsync($"{endpointUrl}?{multiPart}", httpContent).ConfigureAwait(false);
        }
    }
}
