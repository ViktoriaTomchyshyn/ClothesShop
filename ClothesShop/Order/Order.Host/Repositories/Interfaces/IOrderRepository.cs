using System.Threading.Tasks;
using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<int?> CreateOrderAsync(int userId, DateTime createdAt, decimal totalPrice, IEnumerable<OrderItem> items);

        Task<IEnumerable<OrderInfo>> GetOrdersAsync();
        Task<IEnumerable<OrderInfo>> GetOrdersByUserIdAsync(int userId);
    }
}
