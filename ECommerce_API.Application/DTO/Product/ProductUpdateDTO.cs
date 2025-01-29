using ECommerce_API.Core;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_API.Application
{
    public class ProductUpdateDTO
    {
        [Required]
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }

        public Product ToProduct()
        {
            return new Product
            {
                ProductId = ProductId,
                Name = Name!,
                Description = Description!,
                Price = Price,
                Category = Category!
            };
        }
    }
}
