using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Toolsed.Shared;

namespace Toolset.Http.ProGetHome.AssetDirectory.Upload
{
    internal sealed class UploadProGetAsset : BaseProGetAsset, IUploadProGetAsset
    {
        private readonly IWholeFileUploader _wholeFileUploader;
        private readonly IChunkFileUploader _chunkFileUploader;
        private readonly int _chunkSize;

        public UploadProGetAsset(ProGetHomeOptions proGetHomeOptions
            , IWholeFileUploader wholeFileUploader
            , IChunkFileUploader chunkFileUploader
            ) : base(proGetHomeOptions, $"endpoints/{proGetHomeOptions.AssetName.AppendIfNotPresent('/')}content/")
        {
            _wholeFileUploader = wholeFileUploader;
            _chunkFileUploader = chunkFileUploader;
            _chunkSize = proGetHomeOptions.ChunkSize;
        }

        public async Task<HttpResponseMessage> UploadFile(string fileRootPath, string destinationPath)
        {
            if (!File.Exists(fileRootPath)) { throw new FileNotFoundException(nameof(fileRootPath));  }

            var fileName = Path.GetFileName(fileRootPath);
            var endpointUrl = $"{ProGetUrl}{destinationPath}/{fileName}";

            var fileLength = new FileInfo(fileRootPath).Length;

            using var fileStream = File.OpenRead(fileRootPath);

            return fileLength <= _chunkSize
                ? await _wholeFileUploader.Upload(fileStream, endpointUrl).ConfigureAwait(false)
                : await _chunkFileUploader.Upload(fileStream, endpointUrl, fileLength).ConfigureAwait(false);
        }
    }
}
