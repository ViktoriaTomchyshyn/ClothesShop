using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<BasketService> _logger;

        public BasketService(IHttpClientService httpClient, ILogger<BasketService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task AddItem(ItemBasket item)
        {
            _logger.LogInformation(item.ToString());
            var result = await _httpClient.SendAsync<object, ItemRequest<ItemBasket>>(
                $"{_settings.Value.BasketUrl}/additem",
                HttpMethod.Post,
                new ItemRequest<ItemBasket> { Item = item });
        }

        public async Task<IEnumerable<ItemBasket>> GetItems()
        {
            var result = await _httpClient.SendAsync<ItemsResponse<ItemBasket>, object>(
                $"{_settings.Value.BasketUrl}/getitems",
                HttpMethod.Post,
                new { });

            _logger.LogInformation(result.ToString());

            return result.Items;
        }

        public async Task DeleteItems(int id, int amount)
        {
            var result = await _httpClient.SendAsync<object, DeleteItemsRequest>(
                $"{_settings.Value.BasketUrl}/deleteitem",
                HttpMethod.Post,
                new DeleteItemsRequest { ItemId = id, Amount = amount });
        }
    }
}
