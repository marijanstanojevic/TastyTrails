using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Infrastructure.Persistence.EntityConfigurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
               .Property(x => x.Price)
               .IsRequired()
               .HasPrecision(19, 4);
        }
    }
}
