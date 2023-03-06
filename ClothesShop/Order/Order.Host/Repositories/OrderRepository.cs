using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _context = dbContextWrapper.DbContext;
        }

        public async Task<int?> CreateOrderAsync(string userId, DateTime createdAt, decimal totalPrice, IEnumerable<OrderItem> items)
        {
            var order = new OrderInfo()
            {
                Date = createdAt,
                UserId = userId,
                TotalPrice = totalPrice,
                Status = Data.Enums.OrderStatus.New,
                OrderItems = items.ToList()
            };

            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return result.Entity.OrderId;
        }

        public async Task<IEnumerable<OrderInfo>> GetOrdersAsync()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<OrderInfo>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ToListAsync();

            return orders;
        }
    }
}
