using Entities;

namespace Interfaces.DTO.ProductDTO
{
    public class ProductUpdateDTO
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } //check field again if nullable or not
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
