using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem").HasKey(p => p.Id);
            builder.Property(p => p.Id).UseHiLo("order_hilo").IsRequired();

            builder.Property(p => p.ItemId).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Amount).IsRequired();

            builder.HasOne(o => o.OrderInfo)
                            .WithMany(i => i.OrderItems)
                            .HasForeignKey(o => o.OrderId)
                            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
