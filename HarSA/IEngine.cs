using HarSA.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace HarSA
{
    public interface IEngine
    {
        IServiceProvider ConfigureEngineServices(IServiceCollection services, IConfiguration configuration, HarConfig harConfig);

        void ConfigureEngineRequestPipeline(IApplicationBuilder application);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>();

        object ResolveUnregistered(Type type);
    }
}
