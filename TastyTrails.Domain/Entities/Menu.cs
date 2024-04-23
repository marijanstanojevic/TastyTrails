using TastyTrails.Domain.Common;

namespace TastyTrails.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<MenuItem> Items { get; set; }
    }
}
