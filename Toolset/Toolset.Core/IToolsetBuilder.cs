using Microsoft.Extensions.DependencyInjection;
using System;

namespace Toolset.Core
{
    public interface IToolsetBuilder
    {
        IServiceCollection Services { get; }
        IServiceProvider Build();
        ParamSource ParamSource { get; }
        void SetParamSource(ParamSource paramSource);
    }
}
