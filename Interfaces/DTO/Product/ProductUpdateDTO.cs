namespace Interfaces.DTO.ProductDTO
{
    public class ProductUpdateDTO
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
    }
}
