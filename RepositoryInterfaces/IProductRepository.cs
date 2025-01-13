using Entities;

namespace RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product?>GetProductById(Guid productId);
        Task<bool>DeleteProduct(Guid productId);

    }
}
