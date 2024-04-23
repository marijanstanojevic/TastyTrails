using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Infrastructure.Persistence;
using Refit;
using TastyTrails.Infrastructure.Integrations.Wolt.API;
using TastyTrails.Infrastructure.Integrations.Wolt.Handlers;
using TastyTrails.Application.Orders.Interfaces;
using TastyTrails.Infrastructure.Integrations.Wolt.Services;
using TastyTrails.Application.Common.Enums;
using TastyTrails.Infrastructure.Integrations.Services;
using TastyTrails.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddSingleton<IClock, DateTimeOffsetClock>()
                .RegisterPersistence(configuration)
                .RegisterIntegrations(configuration);
        }

        private static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TastyTrails");

            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

            services
                .AddDbContext<TastyTrailsDbContext>(opts => opts.UseSqlServer(connectionString))
                .AddScoped<ITastyTrailsDbContext>(sp => sp.GetRequiredService<TastyTrailsDbContext>())
                .AddScoped<DataSeed>();

            return services;
        }

        private static IServiceCollection RegisterIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            var woltBaseUrl = configuration.GetRequiredSection("Integrations:Wolt:BaseUrl").Value;
            
            services
                .AddRefitClient<IWoltOrderAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(woltBaseUrl))
                .AddHttpMessageHandler<WoltAuthorizationHandler>();

            services.AddTransient<WoltAuthorizationHandler>();

            services.AddKeyedScoped<IChannelOrderDetailsProvider, WoltOrderDetailsProvider>(ExternalChannel.Wolt);

            services.AddScoped<IChannelProviderFactory, KeyedChannelProviderFactory>();
            return services;
        }
    }
}
