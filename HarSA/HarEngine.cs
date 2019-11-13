using Autofac;
using Autofac.Extensions.DependencyInjection;
using HarSA.Configurations;
using HarSA.Exceptions;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HarSA
{
    public class HarEngine : IEngine
    {
        public ILifetimeScope AutofacContainer { get; set; }

        public void ConfigureEngineServices(IServiceCollection services, IConfiguration configuration, HarConfig config)
        {
            var typeFinder = new AppDomainTypeFinder();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();
            var instances = startupConfigurations.Select(t => (IAppStartup)Activator.CreateInstance(t)).OrderBy(o => o.Order);
            foreach (var instance in instances)
            {
                instance.ConfigureServices(services, configuration);
            }
        }

        public void ConfigureEngineRequestPipeline(IApplicationBuilder application)
        {
            AutofacContainer = application.ApplicationServices.GetAutofacRoot();
            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();
            var instances = startupConfigurations.Select(t => (IAppStartup)Activator.CreateInstance(t)).OrderBy(o => o.Order);

            foreach (var instance in instances)
            {
                instance.Configure(application);
            }
        }

        public void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration)
        {
            var typeFinder = new AppDomainTypeFinder();

            builder.RegisterInstance(this).As<IEngine>().SingleInstance();

            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();
            var instances = startupConfigurations.Select(t => (IAppStartup)Activator.CreateInstance(t)).OrderBy(o => o.Order);
            foreach (var instance in instances)
            {
                instance.ConfigureContainer(builder, configuration);
            }
        }

        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            return AutofacContainer.Resolve(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return AutofacContainer.Resolve<IEnumerable<T>>();
        }

        public object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
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