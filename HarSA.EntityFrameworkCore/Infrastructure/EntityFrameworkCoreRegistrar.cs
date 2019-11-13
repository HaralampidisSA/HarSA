using Autofac;
using HarSA.EntityFrameworkCore.Application;
using HarSA.EntityFrameworkCore.Repositories;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarSA.EntityFrameworkCore.Infrastructure
{
    public class EntityFrameworkCoreRegistrar : IAppStartup
    {
        public int Order => 100;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterGeneric(typeof(BaseRepo<>)).As(typeof(IRepo<>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(CrudService<>)).As(typeof(ICrudService<>)).InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(HarCrudService<>)).As(typeof(IHarCrudService<>)).InstancePerLifetimeScope();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}