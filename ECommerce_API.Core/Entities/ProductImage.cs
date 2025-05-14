﻿namespace ECommerce.Domain.Entities
{
    public class ProductImage
    {
        public Guid ProductImageId { get; set; }
        public Guid ProductId { get; set; }
        public string? ImageUrl { get; set; }

        public Product Product { get; set; }
    }
}
