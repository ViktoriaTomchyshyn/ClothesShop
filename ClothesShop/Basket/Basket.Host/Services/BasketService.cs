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
        _logger.LogInformation($"Item {item.Name} has been added");

    }

    public async Task DeleteItem(string userId, int itemId, int amount)
    {
        var items = await GetItems(userId);

        var existingItem = items.FirstOrDefault(p => p.Id == itemId);

        if (existingItem is not null)
        {
            existingItem.Amount -= amount;

            if (existingItem.Amount <= 0)
            {
                items = items.Where(p => p.Id != itemId);
            }

            await _cacheService.AddOrUpdateAsync(userId, items);
            _logger.LogInformation($"Item {existingItem?.Name} has been deleted");
        }
    }

    public async Task DeleteBasket(string userId)
    {
        var items = await GetItems(userId);

        foreach(var item in items)
        {
            var existingItem = items.FirstOrDefault(p => p.Id == item.Id);

            if (existingItem is not null)
            {
                items = items.Where(p => p.Id != item.Id);

                await _cacheService.AddOrUpdateAsync(userId, items);
                _logger.LogInformation($"Item {existingItem?.Name} has been deleted");
            }
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