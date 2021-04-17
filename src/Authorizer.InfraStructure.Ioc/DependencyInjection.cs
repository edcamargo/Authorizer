using Authorizer.Domain.Repositories;
using Authorizer.InfraStructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authorizer.InfraStructure.Ioc
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionServices(ref IServiceCollection services)
        {
            // services.AddTransient<IUserQueries, UserQueries>();
            // services.AddTransient<IEmailFacade, SendGridFacade>();
        }

        public static void DependencyInjectionRepository(ref IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
