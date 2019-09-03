using Autofac;
using HarSA.Configurations;
using HarSA.Dependency;
using HarSA.EntityFrameworkCore.Application;
using HarSA.EntityFrameworkCore.Repositories;
using Microsoft.Extensions.Configuration;

namespace HarSA.EntityFrameworkCore.Infrastructure
{
    public class EntityFrameworkCoreRegistrar : IDependencyRegistrar
    {
        public int Order => 100;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, HarConfig config, IConfiguration configuration)
        {
            builder.RegisterGeneric(typeof(BaseRepo<>)).As(typeof(IRepo<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(CrudService<>)).As(typeof(ICrudService<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(HarCrudService<>)).As(typeof(IHarCrudService<>)).InstancePerLifetimeScope();
        }
    }
}
