using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class ItemService : BaseDataService<ApplicationDbContext>, IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;

        public ItemService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IItemRepository itemRepository,
            ILogger<ItemService> localLogger)
            : base(dbContextWrapper, logger)
        {
            _itemRepository = itemRepository;
            _logger = localLogger;
        }

        public Task<int?> Add(string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock)
        {
            return ExecuteSafeAsync(() =>
            {
                var result = _itemRepository.Add(name, description, category, brand, size, price, pictureFileName, availableStock);

                _logger.LogInformation($"Item with id ({result}) has been added");
                return result;
            });
        }

        public Task<int?> Delete(int id)
        {
            return ExecuteSafeAsync(() =>
            {
                var result = _itemRepository.Delete(id);
                _logger.LogInformation($"Item with id ({result}) has been deleted");
                return result;
            });
        }

        public Task<int?> Update(int id, string name, string description, string category, string brand, string size, decimal price, string pictureFileName, int availableStock)
        {
            return ExecuteSafeAsync(() =>
            {
                var result = _itemRepository.Update(id, name, description, category, brand, size, price, pictureFileName, availableStock);
                _logger.LogInformation($"Item with id ({result}) has been updated");
                return result;
            });
        }
    }
}
