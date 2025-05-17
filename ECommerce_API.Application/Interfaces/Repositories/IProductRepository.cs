using ECommerce.Application.DTO.Product;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<int> AddProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid productId);
        Task<List<Product>> SearchProduct(ProductRequestDTO productDTO);
        Task<int> DeleteProduct(Guid productId);
        Task<int> AddProductImages(Product product);
        Task<int> RemoveProductImages(Product product);

    }
}
