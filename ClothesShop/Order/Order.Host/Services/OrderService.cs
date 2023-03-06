using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Order.Host.Configurations;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;
using Order.Models.Requests;
using Order.Models.Responses;

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

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
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

        public async Task<int?> CreateOrderAsync(string userId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var response = await _httpClient.SendAsync<ItemsResponse<OrderItemDto>, ItemRequest<string>>(
                    $"{_settings.Value.BasketUrl}/getitems",
                    HttpMethod.Post,
                    new ItemRequest<string> { Item = userId });

                var count = response.Items.Count();
                _logger.LogInformation($"Received {count} items from basket");

                if (count <= 0)
                {
                    throw new BusinessException("Impossible to create order without items");
                }

                var totalPrice = response.Items.Sum(p => p.Price * p.Amount);
                var items = response.Items.Select(_mapper.Map<OrderItem>).ToList();

                return await _orderRepository.CreateOrderAsync(userId, DateTime.Now, totalPrice, items);
            });
        }
    }
}
