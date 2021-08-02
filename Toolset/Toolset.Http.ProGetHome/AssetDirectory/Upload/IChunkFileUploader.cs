using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Toolset.Http.ProGetHome.AssetDirectory.Upload
{
    public interface IChunkFileUploader
    {
        Task<HttpResponseMessage> Upload(FileStream fileStream, string endpointUrl, long fileLength);
    }
}
