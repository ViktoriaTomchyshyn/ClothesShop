using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using IdentityModel;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<CatalogService> _logger;
        private readonly IMapper _mapper;

        public CatalogService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IItemRepository itemRepository,
            ILogger<CatalogService> localLogger,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _itemRepository = itemRepository;
            _logger = localLogger;
            _mapper = mapper;
        }

        public async Task<CatalogResponse> GetItemsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var items = await _itemRepository.GetAsync();
                var result = new List<ItemDto>();
                foreach (var item in items)
                {
                    var mappedItem = _mapper.Map<ItemDto>(item);
                    result.Add(mappedItem);
                }

                _logger.LogInformation($"Found {result.Count} items");
                return new CatalogResponse() { Data = result };
            });
        }

        public async Task<ItemDto> GetItemAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var entity = await _itemRepository.GetItemById(id);
                var item = _mapper.Map<ItemDto>(entity);

                _logger.LogInformation($"Item with id ({entity.Id}) has been found.");

                return item;
            });
        }

        public async Task<IEnumerable<string>> GetBrandsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var brands = await _itemRepository.GetBrandsAsync();

                _logger.LogInformation($"Found {brands.Count()} brands");

                return brands;
            });
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var categories = await _itemRepository.GetCategoriesAsync();

                _logger.LogInformation($"Found {categories.Count()} caegories");

                return categories;
            });
        }
    }
}
