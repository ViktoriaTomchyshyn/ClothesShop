namespace Catalog.Host.Data
{
    using Catalog.Host.Data.Entities;
    using Catalog.Host.Data.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Item> Items { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.ApplyConfiguration(new ItemEntityTypeConfiguration());
        }
    }
}
