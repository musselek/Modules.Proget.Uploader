using System.Net.Http;
using System.Threading.Tasks;

namespace Toolset.Http.ProGetHome.AssetDirectory.Upload
{
    public interface IUploadProGetAsset
    {
        Task<HttpResponseMessage> UploadFile(string fileRootPath, string destinationPath);
    }
}
