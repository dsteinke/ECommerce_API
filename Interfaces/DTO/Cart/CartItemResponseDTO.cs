﻿namespace Interfaces.DTO.Cart
{
    public class CartItemResponseDTO
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
