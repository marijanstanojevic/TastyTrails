using TastyTrails.Domain.Entities;

namespace TastyTrails.Application.Orders.Models
{
    public class OrderDetailItem
    {
        public MenuItem MenuItem { get; init; }
        public int Amount { get; init; }
    }
}
