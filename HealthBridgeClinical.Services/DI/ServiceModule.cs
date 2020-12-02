using HealthBridgeClinical.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HealthBridgeClinical.Services.DI
{
    public static class ServiceModule
    {
        public static void ConfigureServiceModule(this IServiceCollection services)
        {
            services.AddScoped<IHealthBridgeService, HealthBridgeService>();
        }
    }
}
