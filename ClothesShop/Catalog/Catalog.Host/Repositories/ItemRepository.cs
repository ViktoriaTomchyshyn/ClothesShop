using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<ItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock)
        {
            var item = await _dbContext.AddAsync(new Item
            {
                Name = name,
                Description = description,
                Category = category,
                Brand = brand,
                Size = size,
                Price = price,
                PictureFileName = pictureFileName,
                AvailableStock = availableStock
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int?> Delete(int id)
        {
            var item = await _dbContext.Items.FindAsync(id);

            if (item != null)
            {
                _dbContext.Items.Remove(item);

                await _dbContext.SaveChangesAsync();

                return id;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await _dbContext.Items.Select(c => c).ToListAsync();
        }

        public async Task<int?> Update(int id, string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock)
        {
            var item = await _dbContext.Items.FindAsync(id);

            if (item != null)
            {
                item.Name = name;
                item.Description = description;
                item.Category = category;
                item.Brand = brand;
                item.Size = size;
                item.Price = price;
                item.PictureFileName = pictureFileName;
                item.AvailableStock = availableStock;

                await _dbContext.SaveChangesAsync();

                return item.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
