using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application
{
    public class ProductAddDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        public List<IFormFile>? Images { get; set; }
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
