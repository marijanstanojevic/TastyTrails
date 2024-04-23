using Refit;
using TastyTrails.Infrastructure.Integrations.Wolt.DTOs;

namespace TastyTrails.Infrastructure.Integrations.Wolt.API
{
    public interface IWoltOrderAPI
    {
        [Get("/orders")]
        Task<WoltOrderDto> GetOrderByIdAsync([AliasAs("order_id"), Query] int orderId);
    }
}
