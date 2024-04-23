using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Application.Orders.Interfaces;
using TastyTrails.Application.Orders.Models;
using TastyTrails.Domain.Enums;
using TastyTrails.Infrastructure.Integrations.Wolt.API;

namespace TastyTrails.Infrastructure.Integrations.Wolt.Services
{
    public class WoltOrderDetailsProvider : IChannelOrderDetailsProvider
    {
        private readonly IWoltOrderAPI _orderAPI;
        private readonly ITastyTrailsDbContext _dbContext;

        public WoltOrderDetailsProvider(IWoltOrderAPI orderAPI, ITastyTrailsDbContext dbContext)
        {
            _orderAPI = orderAPI;
            _dbContext = dbContext;
        }

        public async Task<OrderDetails> GetOrderDetailsAsync(string id, CancellationToken cancellationToken = default)
        {
            if(!int.TryParse(id, out var orderId))
            {
                throw new ArgumentException("Not a valid integer.", nameof(id));
            }
            
            var order = await _orderAPI.GetOrderByIdAsync(orderId);

            var originIds = order.OrderItems
                .Select(x => x.MerchantInfo.OriginId)
                .Distinct()
                .ToList();

            var orderDetailItems = _dbContext.MenuItems
                .Where(x => originIds.Contains(x.Id))
                .ToList()
                .Join(order.OrderItems,
                    m => m.Id,
                    o => o.MerchantInfo.OriginId, (m, o) => new OrderDetailItem
                    {
                        Amount = o.Amount,
                        MenuItem = m
                    })
                .ToArray();

            return new ChannelOrderDetails()
            {
                Items = orderDetailItems
            };
        }
    }
}
