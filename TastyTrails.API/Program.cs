
using TastyTrails.API.Middlewares;
using TastyTrails.Infrastructure.Persistence;

namespace TastyTrails.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .RegisterInfrastructure(builder.Configuration)
                .RegisterApplication()
                .RegisterOptions(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var shouldEnableOutputCache = builder.Configuration.GetValue<bool>("Caching:EnableOutputCache");

            if (shouldEnableOutputCache)
            {
                ConfigureOutputCache(builder.Services, builder.Configuration);
            }

            var app = builder.Build();

            await ExecuteDatabaseSeedingAsync(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (shouldEnableOutputCache)
            {
                app.UseOutputCache();
            }
            
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            
            app.Run();
        }

        private static void ConfigureOutputCache(IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetValue<string>("Caching:Redis:Url");
            var port = configuration.GetValue<string>("Caching:Redis:Port");

            ArgumentException.ThrowIfNullOrWhiteSpace(url, "Caching:Redis:Url");
            ArgumentException.ThrowIfNullOrWhiteSpace(port, "Caching:Redis:Port");

            var redisConfiguration = $"{url}:{port}";
            
            services
                .AddOutputCache()
                .AddStackExchangeRedisOutputCache(cfg =>
                {
                    cfg.InstanceName = "TastyTrails.API";
                    cfg.Configuration = redisConfiguration;
                });
        }

        private static async Task ExecuteDatabaseSeedingAsync(WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            var dataSeed = scope.ServiceProvider.GetRequiredService<DataSeed>();

            await dataSeed.SeedAsync();
        }
    }
}
