using Entities;

namespace Interfaces.DTO.ProductDTO
{
    public class ProductAddDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
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
