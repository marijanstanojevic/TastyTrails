using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TastyTrails.Infrastructure.Integrations.Wolt.Configurations;

namespace TastyTrails.Infrastructure.Integrations.Wolt.Handlers
{
    public class WoltAuthorizationHandler : DelegatingHandler
    {
        private readonly IOptions<WoltIntegrationConfiguration> _config;
        private readonly ILogger<WoltAuthorizationHandler> _logger;

        public WoltAuthorizationHandler(IOptions<WoltIntegrationConfiguration> config, ILogger<WoltAuthorizationHandler> logger)
        {
            _config = config;
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Authorizing request to Wolt api");

            request.Headers.Add("X-API-KEY", _config.Value.ApiKey);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
