using HealthBridgeClinical.Common.Rest;
using Microsoft.Extensions.DependencyInjection;

namespace HealthBridgeClinical.Common.DI
{
    public static class CommonModule
    {
        public static void ConfigureCommonModule(this IServiceCollection services)
        {
            services.AddScoped<IRestClient, RestSharpClient>();
        }
    }
}
