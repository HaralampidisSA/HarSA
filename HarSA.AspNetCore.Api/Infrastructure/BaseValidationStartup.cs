using Autofac;
using FluentValidation;
using HarSA.AspNetCore.Api.FluentValidation;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace HarSA.AspNetCore.Api.Infrastructure
{
    public abstract class BaseValidationStartup : IAppStartup
    {
        public virtual int Order => 10001;

        public virtual void Configure(IApplicationBuilder application)
        {
        }

        public virtual void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            var typeFinder = new AppDomainTypeFinder();
            containerBuilder.RegisterType<HarFluentValidationValidatorFactory>().As<IValidatorFactory>().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(typeFinder.GetAssemblies().ToArray()).AsClosedTypesOf(typeof(IValidator<>)).InstancePerLifetimeScope();
        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(s => s.Errors.Select(c => c.ErrorMessage)).ToList();
                    var result = new
                    {
                        Message = "Validation Errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}