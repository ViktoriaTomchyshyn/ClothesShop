using System.ComponentModel.DataAnnotations;
using Order.Host.Data.Entities;
using Order.Host.Data.Enums;

namespace Order.Host.Models.Dtos
{
    public class OrderDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string? UserId { get; set; }

        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
