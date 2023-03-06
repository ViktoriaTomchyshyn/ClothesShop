﻿using Order.Host.Data.Entities;

namespace Order.Host.Models.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public OrderInfo OrderInfo { get; set; } = null!;
    }
}
