﻿namespace ECommerce_API.Core
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        //Navigation Properties
        public ICollection<CartItem> CartItems { get; set; }
    }
}
