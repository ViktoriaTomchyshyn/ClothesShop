using MVC.Host.Data.Enums;
using MVC.Host.Models.Dtos;

namespace MVC.ViewModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? UserId { get; set; }

        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
