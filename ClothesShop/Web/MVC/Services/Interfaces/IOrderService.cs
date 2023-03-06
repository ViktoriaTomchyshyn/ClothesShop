using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int?> CreateOrderAsync();
        Task<IEnumerable<Order>> GetOrdersAsync();
    }
}
