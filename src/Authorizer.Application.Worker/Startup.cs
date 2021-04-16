using Authorizer.Application.Worker.Servives;
using Authorizer.InfraStructure.Data.Context;
using Authorizer.InfraStructure.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace Authorizer.Application.Worker
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            IConfiguration configuration = builder.Build();

            services.AddSingleton(configuration);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddSerilog();
            });

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

            DependencyInjection.DependencyInjectionServices(ref services);
            DependencyInjection.DependencyInjectionRepository(ref services);

            services.AddSingleton<IAccount, Account>();

            services.AddSingleton<EntryPoint>();

            return services;
        }
    }
}
