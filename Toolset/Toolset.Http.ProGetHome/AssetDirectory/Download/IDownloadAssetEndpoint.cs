using System.Threading.Tasks;

namespace Toolset.Http.ProGetHome.AssetDirectory.Get
{
    public interface IDownloadAssetEndpoint
    {
        Task<byte[]> Downloald(string assetEndPoint);
    }
}