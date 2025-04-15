using ECommerce.Application.DTO.Product;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<bool> AddProduct(ProductAddDTO productAddDTO);
        Task<List<ProductResponseDTO>> GetAllProducts();
        Task<ProductResponseDTO> GetProductById(Guid productId);
        Task<List<ProductResponseDTO>> SearchProducts(ProductRequestDTO productDTO);
        Task<bool>UpdateProduct(ProductUpdateDTO? productUpdateDTO);
        Task<bool>DeleteProduct(Guid productId);
    }
}
