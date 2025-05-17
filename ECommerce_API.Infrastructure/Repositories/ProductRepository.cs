using ECommerce.Application.DTO.Product;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
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

        public async Task<int> AddProduct(Product product)
        {
            _context.Products.Add(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            return await _context.Products
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
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

        public async Task<int> UpdateProduct(Product product)
        {
            var productToUpdate = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

            if (productToUpdate == null)
                return 0;

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.Category = product.Category;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProduct(Guid productId)
        {
            var productToDelete =
                await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            _context.Products.Remove(productToDelete);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddProductImages(Product product)
        {
            _context.ProductImages.AddRange(product.ProductImages);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveProductImages(Product product)
        {
            _context.ProductImages.RemoveRange(product.ProductImages);

            return await _context.SaveChangesAsync();
        }
    }
}
