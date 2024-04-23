using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Application.Orders.Interfaces;
using TastyTrails.Application.Orders.Models;
using TastyTrails.Domain.Entities;
using TastyTrails.Domain.Enums;

namespace TastyTrails.Application.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly ITastyTrailsDbContext _dbContext;

        public OrderService(ITastyTrailsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(OrderDetails orderDetails, CancellationToken cancellationToken = default)
        {

            var orderItems = orderDetails.Items
                .Select(x => new OrderItem
                {
                    Amount = x.Amount,
                    Item = x.MenuItem
                })
                .ToList();

            var totalPrice = orderItems.Sum(x => x.Amount * x.Item.Price);

            var paymentStatus = orderDetails.Type == OrderType.Channel ? PaymentStatus.Paid : PaymentStatus.Unpaid;

            var order = new Order
            {
                Status = OrderStatus.Created,
                Type = orderDetails.Type,
                TotalPrice = totalPrice,
                Items = orderItems,
                PaymentStatus = paymentStatus
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
