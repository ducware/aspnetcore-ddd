using DDD.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
