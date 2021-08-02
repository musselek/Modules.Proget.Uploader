using Modules.Proget.Uploader.Extensions;
using Polly;
using System.IO;
using System.Net.Http;

namespace Modules.Proget.Uploader.ActionHandlers
{
    public static class MessagePreparer
    {
        public static string Prepare(DelegateResult<HttpResponseMessage> httpResponseMessage)
        {
            var msg = httpResponseMessage.Exception?.Message;
            if (!msg.HasValue())
            {
                var stream = new StreamReader(httpResponseMessage.Result.Content.ReadAsStream());
                msg = stream.ReadToEnd();
            }

            return msg ?? string.Empty;
        }
    }
}
