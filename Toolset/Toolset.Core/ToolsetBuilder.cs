using Microsoft.Extensions.DependencyInjection;
using System;

namespace Toolset.Core
{
    public class ToolsetBuilder : IToolsetBuilder
    {
        public IServiceCollection Services { get; }
        public ParamSource ParamSource { get; private set; }
        
        private ToolsetBuilder(IServiceCollection services, ParamSource paramSource)
            => (Services, ParamSource) = (services, paramSource);

        public static IToolsetBuilder Create(IServiceCollection services, ParamSource paramSource)
            => new ToolsetBuilder(services, paramSource);

        public void SetParamSource(ParamSource paramSource)
            =>  ParamSource = paramSource;

        public IServiceProvider Build()
            => Services.BuildServiceProvider();
    }
}