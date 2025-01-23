﻿using Entities;
using System.ComponentModel.DataAnnotations;

namespace Interfaces.DTO.ProductDTO
{
    public class ProductAddDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; } = null!;

        public Product ToProduct()
        {
            return new Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Category = Category
            };
        }
    }
}
