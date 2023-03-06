using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<OrderInfo>
    {
        public void Configure(EntityTypeBuilder<OrderInfo> builder)
        {
            builder.ToTable("OrderInfo").HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId)
                .UseHiLo("order_hilo")
                .IsRequired();

            builder.Property(o => o.Date)
                .IsRequired(true);

            builder.Property(o => o.UserId)
                .IsRequired(true);

            builder.Property(o => o.TotalPrice)
                .IsRequired();
        }
    }
}
