using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;

namespace Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDbContext _context;

        public CartRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddItemToCart(Guid userId, Guid productId, int quantity)
        {
            var cart = await GetCartByUserId(userId);

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
                existingItem.Quantity += quantity;

            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity,
                };

                cart.CartItems.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUserId(Guid userId)
        {
            var cart = await _context.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart == null)
                throw new KeyNotFoundException($"No cart found for user with Id: {userId}.");

            return cart;
        }

        public async Task RemoveItemFromCart(Guid userId, Guid productId)
        {
            var cart = await GetCartByUserId(userId);

            var itemToRemove = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);

            if (itemToRemove == null)
                throw new KeyNotFoundException($"No product with Id: {productId} found in the cart for user with Id: {userId}.");

            _context.CartItems.Remove(itemToRemove);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCartItemQuantity(Guid userId, Guid productId, int quantity)
        {
            var cart = await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart == null)
                return false;

            var itemToUpdate = cart.CartItems
                .FirstOrDefault(x => x.ProductId == productId);

            if (itemToUpdate == null)
                return false;

            itemToUpdate.Quantity = quantity;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
