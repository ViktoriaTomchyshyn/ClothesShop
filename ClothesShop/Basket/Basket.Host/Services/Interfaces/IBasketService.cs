using Basket.Host.Models;

public interface IBasketService
{
    Task AddItem(string userId, Item item);
    Task DeleteItem(string userId, int itemId, int amount);
    Task DeleteBasket(string userId);
    Task<IEnumerable<Item>> GetItems(string userId);
}