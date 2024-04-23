using TastyTrails.Domain.Common;

namespace TastyTrails.Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public ICollection<Menu> Menu { get; set; }
    }
}
