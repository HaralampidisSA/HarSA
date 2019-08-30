using Autofac;
using HarSA.Configurations;
using Microsoft.Extensions.Configuration;

namespace HarSA.Dependency
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, HarConfig config, IConfiguration configuration);

        int Order { get; }
    }
}
