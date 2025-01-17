using Entities;

namespace RepositoryInterfaces
{
    public interface ICartRepository
    {
        Task AddCart(Cart cart);
        Task AddCartItemToCart(Guid userId, Guid productId, int quantity);
        Task<bool> RemoveCartItemFromCart(Guid userId, Guid productId);
        Task UpdateCartItemQuantity(Guid userId, Guid productId, int quantity);
        Task<Cart>GetCartByUserId(Guid userId);
    }
}
