using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Order.Host.Configurations;
using Order.Host.Data;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInternalHttpClientService _httpClient;
        private readonly ILogger<OrderService> _logger;
        private readonly IOptions<OrderConfig> _settings;
        private readonly IMapper _mapper;
        public OrderService(
             IOrderRepository orderRepository,
             IInternalHttpClientService httpClient,
             ILogger<OrderService> logger,
             IOptions<OrderConfig> settings,
             IMapper mapper,
             IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
             ILogger<BaseDataService<ApplicationDbContext>> baseServiceLogger)
            : base(dbContextWrapper, baseServiceLogger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _httpClient = httpClient;
            _settings = settings;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orderEntities = await _orderRepository.GetOrdersByUserIdAsync(userId);
                var orders = orderEntities.Select(_mapper.Map<OrderDto>).ToList();
                return orders;
            });
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orderEntities = await _orderRepository.GetOrdersAsync();
                var orders = orderEntities.Select(_mapper.Map<OrderDto>).ToList();
                return orders;
            });
        }
    }
}
