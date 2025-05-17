using ECommerce.Application.DTO.Product;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task AddProduct(ProductAddDTO productAddDTO);
        Task<List<ProductResponseDTO>> GetAllProducts();
        Task<ProductResponseDTO> GetProductById(Guid productId);
        Task<List<ProductResponseDTO>> SearchProducts(ProductRequestDTO productDTO);
        Task UpdateProduct(ProductUpdateDTO productUpdateDTO);
        Task DeleteProduct(Guid productId);
    }
}
