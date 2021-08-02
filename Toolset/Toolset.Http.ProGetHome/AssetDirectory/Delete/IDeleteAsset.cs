using System.Net.Http;
using System.Threading.Tasks;

namespace Toolset.Http.ProGetHome.AssetDirectory.Delete
{
    public interface IDeleteAsset
    {
        Task<HttpResponseMessage> Delete(string assetRootPath);
    }
}
