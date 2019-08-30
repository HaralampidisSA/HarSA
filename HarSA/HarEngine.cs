using Autofac;
using Autofac.Extensions.DependencyInjection;
using HarSA.Configurations;
using HarSA.Dependency;
using HarSA.Exceptions;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HarSA
{
    public class HarEngine : IEngine
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            var context = accessor.HttpContext;

            return context != null ? context.RequestServices : ServiceProvider;
        }

        protected virtual IServiceProvider RegisterDependencies(IServiceCollection services, ITypeFinder typeFinder, HarConfig config, IConfiguration configuration)
        {
            var containerBuilder = new ContainerBuilder();

            // register engine
            // services.AddSingleton<IEngine>(this);
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();
            // register typeFinder
            // services.AddSingleton(typeFinder);
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //populate Autofac container builder with the set of registered service descriptors
            containerBuilder.Populate(services);

            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            var instances = dependencyRegistrars.Select(registrar => (IDependencyRegistrar)Activator.CreateInstance(registrar)).OrderBy(registrar => registrar.Order);

            foreach (var instance in instances)
            {
                instance.Register(containerBuilder, typeFinder, config, configuration);
            }

            // create service provider
            // _serviceProvider = services.BuildServiceProvider();
            ServiceProvider = new AutofacServiceProvider(containerBuilder.Build());
            return ServiceProvider;
        }

        public IServiceProvider ConfigureEngineServices(IServiceCollection services, IConfiguration configuration, HarConfig config)
        {
            var typeFinder = new AppDomainTypeFinder();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startupConfigurations.Select(startup => (IAppStartup)Activator.CreateInstance(startup)).OrderBy(startup => startup.Order);

            foreach (var instance in instances)
            {
                instance.ConfigureServices(services, configuration);
            }

            RegisterDependencies(services, typeFinder, config, configuration);

            return ServiceProvider;
        }

        public void ConfigureEngineRequestPipeline(IApplicationBuilder application)
        {
            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startupConfigurations.Select(startup => (IAppStartup)Activator.CreateInstance(startup)).OrderBy(startup => startup.Order);

            foreach (var instance in instances)
            {
                instance.Configure(application);
            }
        }

        public T Resolve<T>() where T : class
        {
            return (T)GetServiceProvider().GetRequiredService(typeof(T));
        }

        public object Resolve(Type type)
        {
            return GetServiceProvider().GetRequiredService(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        public object ResolveUnregistered(Type type)
        {
            Exception innerException = null;

            var constructors = type.GetConstructors();

            foreach (var constructor in constructors)
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new HarException("Unknown dependency");
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }
            throw new HarException("No constructor was found that had all the dependencies satisfied.", innerException);
        }
    }
}
