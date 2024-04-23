using TastyTrails.Application.Common.Enums;

namespace TastyTrails.Application.Orders.Interfaces
{
    public interface IChannelProviderFactory
    {
        IChannelOrderDetailsProvider GetProvider(ExternalChannel externalChannel);
    }
}
