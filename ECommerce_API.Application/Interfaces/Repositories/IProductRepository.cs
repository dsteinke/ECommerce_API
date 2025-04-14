using ECommerce.Application.DTO.Product;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories
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
