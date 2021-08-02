using System.Net.Http;
using System.Threading.Tasks;

namespace Toolset.Http.ProGetHome.AssetDirectory.Create
{
    public interface ICreateAssetDirectory
    {
        Task<HttpResponseMessage> Create(string directory);
    }
}
