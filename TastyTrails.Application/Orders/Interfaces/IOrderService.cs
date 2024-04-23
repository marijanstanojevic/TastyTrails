using TastyTrails.Application.Orders.Models;

namespace TastyTrails.Application.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateAsync(OrderDetails orderDetails, CancellationToken cancellationToken = default);
    }
}
