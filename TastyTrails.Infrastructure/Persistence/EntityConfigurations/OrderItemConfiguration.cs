using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Infrastructure.Persistence.EntityConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .Property(x => x.Amount)
                .IsRequired();
        }
    }
}
