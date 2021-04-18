using Authorizer.Domain.Interfaces.Repositories;
using Authorizer.Domain.Interfaces.Services;
using Authorizer.InfraStructure.Data.Repositories;
using Authorizer.InfraStructure.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authorizer.InfraStructure.Ioc
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionServices(ref IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
        }

        public static void DependencyInjectionRepository(ref IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
