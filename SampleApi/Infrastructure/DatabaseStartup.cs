using Autofac;
using HarSA.EntityFrameworkCore;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SampleApi.Infrastructure
{
    public class DatabaseStartup : IAppStartup
    {
        public int Order => 100;

        public void Configure(IApplicationBuilder application)
        {

        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationAssembly = typeof(DatabaseStartup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<HarDbContext>(o => o.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)));
        }
    }
}
