using TastyTrails.Domain.Common;
using TastyTrails.Domain.Enums;

namespace TastyTrails.Domain.Entities
{
    public class Order : BaseEntity
    {
        public OrderType Type { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
