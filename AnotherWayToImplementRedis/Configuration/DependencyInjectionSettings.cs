using AnotherWayToImplementRedis.Interfaces;
using AnotherWayToImplementRedis.Repositories;

namespace AnotherWayToImplementRedis.Configuration
{
    public static class DependencyInjectionSettings
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
