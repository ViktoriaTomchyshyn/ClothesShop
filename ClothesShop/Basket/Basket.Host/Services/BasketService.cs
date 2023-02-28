using Basket.Host.Models;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<BasketService> _logger;

    public BasketService(ICacheService cacheService, ILogger<BasketService> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task AddItem(string userId, Item item)
    {
        if (item.Amount <= 0)
        {
            return;
        }

        var items = (await GetItems(userId)).ToList();

        var existingItems = items.FirstOrDefault(p => p.Id == item.Id);

        if (existingItems != null)
        {
            existingItems.Amount += item.Amount;
        }
        else
        {
            items.Add(item);
        }

        await _cacheService.AddOrUpdateAsync(userId, items);

    }

    public async Task DeleteItem(string userId, int itemId, int amount)
    {
        var items = await GetItems(userId);

        var existingProduct = items.FirstOrDefault(p => p.Id == itemId);

        if (existingProduct is not null)
        {
            existingProduct.Amount -= amount;

            if (existingProduct.Amount <= 0)
            {
                items = items.Where(p => p.Id != itemId);
            }

            await _cacheService.AddOrUpdateAsync(userId, items);

        }
    }

     public async Task<IEnumerable<Item>> GetItems(string userId)
    {
        var items = await _cacheService.GetAsync<IEnumerable<Item>>(userId);

        if (items is null)
        {
            items = new List<Item>();
        }

        _logger.LogInformation($"Found {items.Count()} items");

        return items;
    }
}