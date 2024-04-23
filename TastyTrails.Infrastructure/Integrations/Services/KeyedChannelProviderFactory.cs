using Microsoft.Extensions.DependencyInjection;
using TastyTrails.Application.Common.Enums;
using TastyTrails.Application.Orders.Interfaces;

namespace TastyTrails.Infrastructure.Integrations.Services
{
    public class KeyedChannelProviderFactory : IChannelProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public KeyedChannelProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChannelOrderDetailsProvider GetProvider(ExternalChannel externalChannel)
        {
            var service = _serviceProvider.GetKeyedService<IChannelOrderDetailsProvider>(externalChannel);

            if(service is null)
            {
                throw new NotImplementedException($"Implementation for {externalChannel} not found.");
            }

            return service;
        }
    }
}
