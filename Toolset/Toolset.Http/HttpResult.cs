using System.Net.Http;

namespace Toolset.Http
{
    public class HttpResult<T>
    {
        public T Result { get; }
        public HttpResponseMessage Response { get; }
        public bool HasResult => Result is { };
        public HttpResult(T result, HttpResponseMessage response)
        {
            Result = result;
            Response = response;
        }
    }
}
