using TastyTrails.Domain.Common;

namespace TastyTrails.Domain.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Menu Menu { get; set; }
    }
}
