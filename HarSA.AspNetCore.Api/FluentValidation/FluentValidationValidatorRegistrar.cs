using Autofac;
using FluentValidation;
using HarSA.Configurations;
using HarSA.Dependency;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace HarSA.AspNetCore.Api.FluentValidation
{
    public class FluentValidationValidatorRegistrar : IDependencyRegistrar
    {
        public int Order => 9000;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, HarConfig config, IConfiguration configuration)
        {
            builder.RegisterType<HarFluentValidationValidatorFactory>().As<IValidatorFactory>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeFinder.GetAssemblies().ToArray()).AsClosedTypesOf(typeof(IValidator<>)).InstancePerLifetimeScope();
        }
    }
}
