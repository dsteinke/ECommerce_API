using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application
{
    public class ProductUpdateDTO
    {
        [Required]
        public Guid ProductId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }

        public Product ToProduct()
        {
            return new Product
            {
                ProductId = ProductId,
                Name = Name,
                Description = Description,
                Price = Price,
                Category = Category
            };
        }
    }
}
