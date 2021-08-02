using System.Net.Http;
using System.Threading.Tasks;
using Toolset.Http.ProGetHome.HttpClientService;

namespace Toolset.Http.ProGetHome.AssetDirectory.Upload
{
    public interface IChunkProcessor
    {
        Task<HttpResponseMessage> OnUpload(byte[] buffer
            , string endpointUrl
            , int index
            , int offset
            , long fileLength
            , long currentChunkSize
            , long totalParts
            , IProGetHomeHttpClient proGetHomeHttp);

        Task<HttpResponseMessage> OnComplete(string endpointUrl, IProGetHomeHttpClient proGetHomeHttp);    
    }
}
