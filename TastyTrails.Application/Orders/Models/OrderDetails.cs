using TastyTrails.Domain.Enums;

namespace TastyTrails.Application.Orders.Models
{
    public class DirectOrderDetails : OrderDetails
    {
        public DirectOrderDetails() : base(OrderType.Direct) { }
    }

    public class ChannelOrderDetails : OrderDetails
    {
        public ChannelOrderDetails() : base(OrderType.Channel) { }
    }

    public class OrderDetails
    {
        public OrderType Type { get; set; }
        public OrderDetailItem[] Items { get; set; }

        public OrderDetails(OrderType type)
        {
            Type = type;
        }

        public OrderDetails(OrderType type, OrderDetailItem[] items) : this(type)
        {
            Items = items;
        }
    }
}
