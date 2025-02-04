using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product?>GetProductById(Guid productId);
        Task<List<Product>> SearchProduct(ProductRequestDTO productDTO);
        Task<bool>DeleteProduct(Guid productId);

    }
}
