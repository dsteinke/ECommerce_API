using Interfaces.DTO.ProductDTO;

namespace Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(ProductAddDTO? productAddDTO);
        Task<List<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(Guid productId);
        Task<List<ProductDTO>> SearchProducts();
        Task<ProductDTO>UpdateProduct(ProductUpdateDTO? productUpdateDTO);
        Task<bool>DeleteProduct(Guid productId);
    }
}
