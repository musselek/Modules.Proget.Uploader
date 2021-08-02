using Toolsed.Shared;

namespace Toolset.Http.ProGetHome
{
    public abstract class BaseProGetAsset
    {
        protected string ProGetUrl { get;  }
        public BaseProGetAsset(ProGetHomeOptions proGetHomeOptions, string feedName)
        {
            ProGetUrl = $"{proGetHomeOptions.EndpointAddress.AppendIfNotPresent('/')}{feedName}";
        }
    }
}
