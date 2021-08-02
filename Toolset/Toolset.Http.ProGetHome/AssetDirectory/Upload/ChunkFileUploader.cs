using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Upload
{
    internal sealed class ChunkFileUploader : IChunkFileUploader
    {
        private readonly IProGetHomeHttpClient _proGetHomeHttpClient;
        private readonly IChunkProcessor _chunkProcessor;
        private readonly int _chunkSize;

        public ChunkFileUploader(IProGetHomeHttpClient proGetHomeHttpClient, ProGetHomeOptions proGetHomeOptions, IChunkProcessor chunkProcessor) 
            => (_proGetHomeHttpClient, _chunkSize, _chunkProcessor) = (proGetHomeHttpClient, proGetHomeOptions.ChunkSize, chunkProcessor);

        public async Task<HttpResponseMessage> Upload(FileStream fileStream, string endpointUrl, long fileLength)
        {
            var totalParts = Math.DivRem(fileLength, _chunkSize, out var remainder);
            if (remainder != 0) { ++totalParts; }

            var index = 0;
            var offset = 0;
            var buffer = new byte[_chunkSize];

            fileStream.Seek(0, SeekOrigin.Begin);
            var bytesRead = fileStream.Read(buffer, 0, buffer.Length);

            while (bytesRead > 0)
            {
                var currentChunkSize = index == totalParts - 1
                   ? fileLength - offset
                   : _chunkSize;

                var chunkResponse = await _chunkProcessor.OnUpload(buffer, endpointUrl, index, offset, fileLength, currentChunkSize, totalParts, _proGetHomeHttpClient).ConfigureAwait(false);

                offset = ++index * _chunkSize;
                bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                if (!chunkResponse.IsSuccessStatusCode)  { return chunkResponse; }
            }

            return  await _chunkProcessor.OnComplete(endpointUrl, _proGetHomeHttpClient).ConfigureAwait(false);
        }
    }
}
