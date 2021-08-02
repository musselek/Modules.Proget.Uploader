using Microsoft.Extensions.DependencyInjection;
using Modules.Proget.Uploader.CommandLine;
using Modules.Proget.Uploader.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Toolset.Http.ProGetHome.AssetDirectory.Upload;

namespace Modules.Proget.Uploader.Upload
{
    internal sealed class FileUploader
    {
        private readonly StreamWriter _streamWriter;
        private readonly IUploadProGetAsset _uploadProGetAsset;
        private readonly CommandLineArgs _commandLineOptions;

        public FileUploader(IServiceProvider serviceProvider, CommandLineArgs opts, StreamWriter streamWriter)
            => (_uploadProGetAsset, _commandLineOptions, _streamWriter) = (serviceProvider.GetService<IUploadProGetAsset>(), opts, streamWriter);

        public async Task<int> UpoadAsync()
        {
            string endedStatus = string.Empty;
            HttpStatusCode httpStatusCode = 0;
            var files = _commandLineOptions.Files ?? Enumerable.Empty<string>();

            _streamWriter.WriteLine($"Files to upload: [{files.Count()}]");

            foreach (var (idx, file) in files.WithIndex(1))
            {
                var fileSourcePath = $"{_commandLineOptions.SourceFolder.AppendIfNotExist('/')}{file}";

                _streamWriter.WriteLine($"{idx}. Starting upload [{fileSourcePath}]");
                if (!File.Exists(fileSourcePath))
                {
                    _streamWriter.WriteLine($"File [{fileSourcePath}] doesn't exist");
                    continue;
                }

                try
                {
                    using var responseMsg = await _uploadProGetAsset.UploadFile(fileSourcePath, _commandLineOptions.DestinationFolder).ConfigureAwait(false);
                    httpStatusCode = responseMsg.StatusCode;
                    endedStatus = responseMsg.IsSuccessStatusCode ? "successfully" : "in failure";
                }
                catch
                {
                    endedStatus = "in failure";
                }
                finally
                {
                    _streamWriter.WriteLine($"Upload process for [{fileSourcePath}] ended {endedStatus} with code [{httpStatusCode}]");
                }
            }

            return 0;
        }
    }
}
