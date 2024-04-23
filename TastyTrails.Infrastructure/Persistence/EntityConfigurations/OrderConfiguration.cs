using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Infrastructure.Persistence.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>();

            builder
                .Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>();

            builder
                .HasMany(x => x.Items);

            builder
                .Property(x => x.TotalPrice)
                .IsRequired()
                .HasPrecision(19, 4);

            builder
                .Property(x => x.PaymentStatus)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}