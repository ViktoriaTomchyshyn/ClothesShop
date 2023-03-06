using Order.Host.Data.Enums;

namespace Order.Host.Data.Entities
{
    public class OrderInfo
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
