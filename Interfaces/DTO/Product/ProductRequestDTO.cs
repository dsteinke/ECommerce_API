﻿namespace Interfaces.DTO.ProductDTO
{
    public class ProductRequestDTO
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
    }
}
