using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Toolset.Http.ProGetHome.AssetDirectory.Get
{
    public interface IGetAssetEndpoint
    {
        Task<IEnumerable<AssetDirectoryItem>> Get(string directory, bool recursive);
    }
}
