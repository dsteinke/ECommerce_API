﻿using ECommerce_API.Core.Enums;
using ECommerce_API.Core.Identity;

namespace ECommerce_API.Core
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public decimal TotalAmount {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        //Navigation Properties
        public ApplicationUser User { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
