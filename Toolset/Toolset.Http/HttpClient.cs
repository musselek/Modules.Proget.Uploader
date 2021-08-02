using System.Net.Http;
using System.Threading.Tasks;

namespace Toolset.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<T> GetAsync<T>(string uri);
        Task<HttpResult<T>> GetResultAsync<T>(string uri);
    }
}
