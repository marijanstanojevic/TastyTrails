using TastyTrails.Application.Orders.Models;

namespace TastyTrails.Application.Orders.Interfaces
{
    public interface IChannelOrderDetailsProvider
    {
        Task<OrderDetails> GetOrderDetailsAsync(string id, CancellationToken cancellationToken = default);
    }
}
