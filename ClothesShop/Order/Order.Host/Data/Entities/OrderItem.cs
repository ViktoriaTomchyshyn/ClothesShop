namespace Order.Host.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public OrderInfo OrderInfo { get; set; } = null!;
    }
}
