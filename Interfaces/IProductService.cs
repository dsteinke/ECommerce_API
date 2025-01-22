using Interfaces.DTO.ProductDTO;

namespace Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDTO> AddProduct(ProductAddDTO? productAddDTO);
        Task<List<ProductResponseDTO>> GetAllProducts();
        Task<ProductResponseDTO> GetProductById(Guid productId);
        Task<List<ProductResponseDTO>> SearchProducts(ProductResponseDTO productDTO);
        Task<ProductResponseDTO>UpdateProduct(ProductUpdateDTO? productUpdateDTO);
        Task<bool>DeleteProduct(Guid productId);
    }
}
