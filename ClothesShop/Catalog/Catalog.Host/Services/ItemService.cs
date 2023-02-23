using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class ItemService : BaseDataService<ApplicationDbContext>, IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IItemRepository itemRepository)
            : base(dbContextWrapper, logger)
        {
            _itemRepository = itemRepository;
        }

        public Task<int?> Add(string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock)
        {
            return ExecuteSafeAsync(() => _itemRepository.Add(name, description, category,  brand, size, price, pictureFileName,availableStock));
        }

        public Task<int?> Delete(int id)
        {
            return ExecuteSafeAsync(() => _itemRepository.Delete(id));
        }

        public Task<int?> Update(int id, string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock)
        {
            return ExecuteSafeAsync(() => _itemRepository.Update(id, name, description, category, brand, size, price, pictureFileName, availableStock));
        }
    }
}
