using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toolsed.Shared;

namespace Toolset.Http
{
    public class ToolsetHttpClient : IToolsetHttpClient
    {

        private readonly Action<DelegateResult<HttpResponseMessage>, TimeSpan> _onRetry;

        private static readonly HttpStatusCode[] _httpStatusCodesWorthRetrying = {
                                    HttpStatusCode.RequestTimeout, // 408
                                    HttpStatusCode.InternalServerError, // 500
                                    HttpStatusCode.BadGateway, // 502
                                    HttpStatusCode.ServiceUnavailable, // 503
                                    HttpStatusCode.GatewayTimeout // 504
        };

        protected enum Method
        {
            Get,
            Post,
            Put,
            Patch,
            Delete
        }

        private readonly HttpClient _client;
        private readonly HttpClientOptions _httpClientOptions;

        private static readonly JsonSerializer JsonSerializer = new() { ContractResolver = new CamelCasePropertyNamesContractResolver()};

        public ToolsetHttpClient(HttpClient client, HttpClientOptions httpClientOptions, Action<DelegateResult<HttpResponseMessage>, TimeSpan> onRetry = null)
        {
            _onRetry = onRetry;
            _client = client;
            _httpClientOptions = httpClientOptions;
           
            if(_httpClientOptions.TimeoutInMs > 0)
            {
                _client.Timeout = TimeSpan.FromMilliseconds(_httpClientOptions.TimeoutInMs);
            }
        }

        public virtual Task<HttpResponseMessage> DeleteAsync(string uri)
            => SendAsync(uri, Method.Delete);

        public virtual Task<HttpResponseMessage> GetAsync(string uri)
             => SendAsync(uri, Method.Get);

        public virtual Task<T> GetAsync<T>(string uri)
            => SendAsync<T>(uri, Method.Get);

        public Task<HttpResult<T>> GetResultAsync<T>(string uri)
            => SendResultAsync<T>(uri, Method.Get);

        public virtual Task<HttpResponseMessage> PostAsync(string uri, object data = null)
           => SendAsync(uri, Method.Post, GetJsonPayload(data));

        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
            => SendAsync(uri, Method.Post, content);

        public virtual Task<T> PostAsync<T>(string uri, object data = null)
            => SendAsync<T>(uri, Method.Post, GetJsonPayload(data));

        public Task<T> PostAsync<T>(string uri, HttpContent content)
            => SendAsync<T>(uri, Method.Post, content);

        public Task<HttpResult<T>> PostResultAsync<T>(string uri, object data = null)
            => SendResultAsync<T>(uri, Method.Post, GetJsonPayload(data));

        public Task<HttpResult<T>> PostResultAsync<T>(string uri, HttpContent content)
            => SendResultAsync<T>(uri, Method.Post, content);

        protected virtual async Task<T> SendAsync<T>(string uri, Method method, HttpContent content = null)
        {
            var response = await SendAsync(uri, method, content).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            return GetDataFromStream<T>(stream);
        }

        protected virtual async Task<HttpResult<T>> SendResultAsync<T>(string uri, Method method, HttpContent content = null)
        {
            var response = await SendAsync(uri, method, content).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return new HttpResult<T>(default, response);
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var result = GetDataFromStream<T>(stream);

            return new HttpResult<T>(result, response);
        }

        protected async Task<HttpResponseMessage> SendAsync(string uri, Method method, HttpContent content = null)
            => await Policy.Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => _httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(_httpClientOptions.Retries, r => TimeSpan.FromSeconds(Math.Pow(2, r)), _onRetry)
                .ExecuteAsync(async () =>
                {
                    var requestUri = uri.StartsWith("http") ? uri : $"http://{uri}";
                    var response = await GetResponseAsync(requestUri, method, content).ConfigureAwait(false); ;
                    return response;
                });


        protected async Task<HttpResponseMessage> GetResponseAsync(string uri, Method method, HttpContent content = null)
        {
            return method switch
            {
                Method.Get => await _client.GetAsync(uri).ConfigureAwait(false),
                Method.Post => await _client.PostAsync(uri, content).ConfigureAwait(false),
                Method.Put => await _client.PutAsync(uri, content).ConfigureAwait(false),
                Method.Patch => await _client.PatchAsync(uri, content).ConfigureAwait(false),
                Method.Delete => await _client.DeleteAsync(uri).ConfigureAwait(false),
                _ => throw new InvalidOperationException($"Unsupported HTTP method: {method}"),
            };
        }

        protected static T GetDataFromStream<T>(Stream stream)
        {
            if (stream is null || stream.CanRead is false)
            {
                return default;
            }
            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);

            return typeof(T).Name switch
            {
                "string" => (T)(object)streamReader.ReadToEnd(),
                "Byte[]" => (T)(object)stream.ToByteArray(),
                _ => JsonSerializer.Deserialize<T>(jsonTextReader)
            };
        }

        protected StringContent GetJsonPayload(object data)
        {
            if (data is null)
            {
                return null;
            }

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            return content;
        }
    }
}
 