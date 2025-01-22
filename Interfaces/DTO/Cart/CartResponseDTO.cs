﻿namespace Interfaces.DTO.Cart
{
    public class CartResponseDTO
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public List<CartItemResponseDTO>? CartItems { get; set; }

    }
}
