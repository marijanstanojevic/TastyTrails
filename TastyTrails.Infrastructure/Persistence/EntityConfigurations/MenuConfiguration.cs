using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Infrastructure.Persistence.EntityConfigurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Menu);
        }
    }
}
