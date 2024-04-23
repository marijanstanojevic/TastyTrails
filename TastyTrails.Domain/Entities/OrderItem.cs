using TastyTrails.Domain.Common;

namespace TastyTrails.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Order Order { get; set; }
        public MenuItem Item { get; set; }
        public int Amount { get; set; }
    }
}
