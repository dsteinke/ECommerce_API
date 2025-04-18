﻿namespace ECommerce_API.Application
{
    public class CartItemResponseDTO
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
