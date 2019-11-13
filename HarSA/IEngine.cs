using Autofac;
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
        void ConfigureEngineServices(IServiceCollection services, IConfiguration configuration, HarConfig harConfig);

        void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration);

        void ConfigureEngineRequestPipeline(IApplicationBuilder application);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>();

        object ResolveUnregistered(Type type);
    }
}