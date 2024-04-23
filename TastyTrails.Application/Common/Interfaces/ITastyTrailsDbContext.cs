using Microsoft.EntityFrameworkCore;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Application.Common.Interfaces
{
    public interface ITastyTrailsDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<Restaurant> Restaurants { get; }
        DbSet<Menu> Menus { get; }
        DbSet<MenuItem> MenuItems { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
    }
}
