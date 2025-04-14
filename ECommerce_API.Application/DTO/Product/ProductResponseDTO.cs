﻿namespace ECommerce.Application.DTO.Product
{
    public class ProductResponseDTO
    {
        public Guid? ProductId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public decimal? Price { get; set; }
        public string? Category { get; set; } = null!;
    }
}
