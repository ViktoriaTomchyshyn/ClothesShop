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
            ILogger<CatalogService> localLogger, IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _itemRepository = itemRepository;
            _logger = localLogger;
            _mapper = mapper;
        }

        public async Task<CatalogResponse> GetAll()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var items = await _itemRepository.GetAsync();
                var result = new List<ItemDto>();
                foreach (var item in items)
                {
                    var newItem = new ItemDto();
                    var mappedItem = _mapper.Map<ItemDto>(newItem);
                    result.Add(mappedItem);
                }
                _logger.LogInformation($"Found {result.Count} items");
                return new CatalogResponse() { Data = result };
            });
        }
    }
}
