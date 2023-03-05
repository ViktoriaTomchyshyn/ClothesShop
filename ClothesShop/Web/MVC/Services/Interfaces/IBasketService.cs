using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddItem(ItemBasket item);
        Task<IEnumerable<ItemBasket>> GetItems();
        Task DeleteItems(int id, int amount);
    }
}
