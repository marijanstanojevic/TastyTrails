using TastyTrails.Infrastructure.Integrations.Wolt.Configurations;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OptionsRegistration
    {
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions<WoltIntegrationConfiguration>()
                .BindConfiguration("Integrations:Wolt")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
    }
}
