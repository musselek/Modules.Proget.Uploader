using Polly;
using System;
using System.Net.Http;

namespace Toolset.Http.ProGetHome.HttpClientService
{
    internal sealed class ProGetHomeHttpClient : ToolsetHttpClient, IProGetHomeHttpClient
    {
        public ProGetHomeHttpClient(HttpClient client
            , HttpClientOptions httpClientOptions
            , ProGetHomeOptions proGetHomeOptions
            , Action<DelegateResult<HttpResponseMessage>, TimeSpan> onRetry = null)
             : base(client, httpClientOptions, onRetry)
        {
            client.DefaultRequestHeaders.Add("X-ApiKey", proGetHomeOptions.ApiKey);
        }
    }
}
