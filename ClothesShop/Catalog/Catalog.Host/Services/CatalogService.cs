using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly IItemRepository _itemRepository;

        public CatalogService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IItemRepository itemRepository)
            : base(dbContextWrapper, logger)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var items = await _itemRepository.GetAsync();
                var result = new List<ItemDto>();
                foreach (var item in items)
                {
                    result.Add(new ItemDto() { Id = item.Id, AvailableStock = item.AvailableStock, Description = item.Description, Name = item.Name, Price = item.Price, Brand = item.Brand, Category = item.Category, Size = item.Size, PictureFileName = item.PictureFileName });
                }
                return result;
            });
        }
    }
}
