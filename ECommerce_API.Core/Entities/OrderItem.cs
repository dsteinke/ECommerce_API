﻿namespace ECommerce.Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //Navigation Properties
        public Order Order { get; set; }
    }
}
