using Autofac;
using HarSA.AspNetCore.Installation;
using HarSA.Configurations;
using HarSA.Dependency;
using Microsoft.Extensions.Configuration;

namespace HarSA.AspNetCore.Infrastructure.Dependency
{
    public class InstallationRegistrar : IDependencyRegistrar
    {
        public int Order => 100;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, HarConfig config, IConfiguration configuration)
        {
            builder.RegisterType<ApplicationService>().As<IApplicationService>().InstancePerLifetimeScope();
        }
    }
}
