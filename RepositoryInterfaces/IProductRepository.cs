using Entities;
using Interfaces.DTO.ProductDTO;
using System.Threading.Tasks;

namespace RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product?>GetProductById(Guid productId);
        Task<List<Product>> SearchProduct(ProductDTO productDTO);
        Task<bool>DeleteProduct(Guid productId);

    }
}
