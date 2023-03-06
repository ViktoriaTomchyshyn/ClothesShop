namespace Order.Host.Data
{
    using Order.Host.Data.Entities;
    using Order.Host.Data.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OrderInfo> Orders { get; set; } = null!;

        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.ApplyConfiguration(new OrderConfigurations());
           builder.ApplyConfiguration(new OrderItemConfigurations());
        }
    }
}
