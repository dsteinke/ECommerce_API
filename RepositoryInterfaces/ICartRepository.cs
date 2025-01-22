using Entities;

namespace RepositoryInterfaces
{
    public interface ICartRepository
    {
        Task AddItemToCart(Guid userId, Guid productId, int quantity);
        Task RemoveItemFromCart(Guid userId, Guid productId);
        Task<bool> UpdateCartItemQuantity(Guid userId, Guid productId, int quantity);
        Task<Cart> GetCartByUserId(Guid userId);
    }
}
