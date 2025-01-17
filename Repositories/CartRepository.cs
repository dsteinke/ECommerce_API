using Entities;
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

        public async Task AddCart(Cart cart)
        {
            _context.Carts.Add(cart);

            await _context.SaveChangesAsync();
        }

        public Task AddCartItemToCart(Guid userId, Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCartByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCartItemFromCart(Guid userId, Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItemQuantity(Guid userId, Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
