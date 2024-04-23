using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Infrastructure.Persistence.EntityConfigurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
                .Property(x => x.Location)
                .IsRequired();

            builder
                .HasMany(x => x.Menu)
                .WithOne(x => x.Restaurant);
        }
    }
}
