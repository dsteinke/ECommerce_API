using ECommerce.Application.DTO.Product;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<List<Product>> SearchProduct(ProductRequestDTO productDTO)
        {
            var query = _context.Products.AsQueryable();

            if (productDTO.ProductId != null)
            {
                query = query.Where(x => x.ProductId == productDTO.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(productDTO.Name))
            {
                query = query.Where(x => x.Name.Contains(productDTO.Name));
            }
            if (!string.IsNullOrWhiteSpace(productDTO.Description))
            {
                query = query.Where(x => x.Description.Contains(productDTO.Description));
            }
            if (productDTO.Price > 0)
            {
                query = query.Where(x => x.Price == productDTO.Price);
            }
            if (!string.IsNullOrWhiteSpace(productDTO.Category))
            {
                query = query.Where(x => x.Category.Contains(productDTO.Category));
            }

            return await query.ToListAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var productToUpdate = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

            if (productToUpdate == null)
                return product;

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.Category = product.Category;

            await _context.SaveChangesAsync();

            return productToUpdate;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var productToDelete =
                await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            _context.Products.Remove(productToDelete);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
