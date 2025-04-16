using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDbContext _context;

        public CartRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> AddCartToUser(ApplicationUser user)
        {
            var cart = new Cart { UserId = user.Id };
            _context.Carts.Add(cart);

            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<int> AddItemToCart(CartItem item)
        {
            _context.CartItems.Add(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCartItem(CartItem item)
        {
            _context.CartItems.Update(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<Cart?> GetCartByUserId(Guid userId)
        {
            var cart = await _context.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            return cart;
        }

        public async Task<int> RemoveItemFromCart(CartItem item)
        {
            _context.CartItems.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCartItemQuantity(Guid userId, Guid productId, int quantity)
        {
            var itemToUpdate = await _context.CartItems
                .Include(x => x.Cart)
                .FirstOrDefaultAsync(x => x.Cart.UserId == userId && x.ProductId == productId);

            itemToUpdate.Quantity = quantity;

            return await _context.SaveChangesAsync();
        }

        public async Task ClearCart(Guid userId)
        {
            var cart = await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            _context.CartItems.RemoveRange(cart.CartItems);
        }
    }
}
