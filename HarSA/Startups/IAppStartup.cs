﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarSA.Startups
{
    public interface IAppStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration);

        void Configure(IApplicationBuilder application);

        int Order { get; }
    }
}
