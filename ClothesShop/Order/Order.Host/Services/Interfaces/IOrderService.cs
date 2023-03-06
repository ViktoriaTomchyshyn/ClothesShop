using System.Threading.Tasks;
using Order.Host.Models.Dtos;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        Task<int?> CreateOrderAsync(string userId);
    }
}
