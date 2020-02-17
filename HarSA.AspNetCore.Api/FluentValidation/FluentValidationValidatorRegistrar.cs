using Autofac;
using FluentValidation;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace HarSA.AspNetCore.Api.FluentValidation
{
    public class FluentValidationValidatorRegistrar : IAppStartup
    {
        public int Order => 9000;

        public void Configure(IApplicationBuilder application)
        {

        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();

            containerBuilder.RegisterType<HarFluentValidationValidatorFactory>().As<IValidatorFactory>().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(typeFinder.GetAssemblies().ToArray()).AsClosedTypesOf(typeof(IValidator<>)).InstancePerLifetimeScope();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }

    }
}
