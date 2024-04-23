using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Infrastructure.Persistence
{
    public class TastyTrailsDbContext : DbContext, ITastyTrailsDbContext
    {
        public TastyTrailsDbContext(DbContextOptions<TastyTrailsDbContext> opts) : base(opts)
        {

        }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();

        public DbSet<Menu> Menus => Set<Menu>();

        public DbSet<MenuItem> MenuItems => Set<MenuItem>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
