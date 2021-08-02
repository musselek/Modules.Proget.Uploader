using System.Net.Http;
using System.Threading.Tasks;

namespace Toolset.Http
{
    public interface IToolsetHttpClient
    {
        Task<HttpResponseMessage> DeleteAsync(string uri);
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<T> GetAsync<T>(string uri);
        Task<HttpResult<T>> GetResultAsync<T>(string uri);
        Task<HttpResponseMessage> PostAsync(string uri, object data = null);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
        Task<T> PostAsync<T>(string uri, object data = null);
        Task<T> PostAsync<T>(string uri, HttpContent content);
        Task<HttpResult<T>> PostResultAsync<T>(string uri, object data = null);
        Task<HttpResult<T>> PostResultAsync<T>(string uri, HttpContent content);
    }
}
