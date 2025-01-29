using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public interface ICartRepository
    {
        Task AddItemToCart(Guid userId, Guid productId, int quantity);
        Task RemoveItemFromCart(Guid userId, Guid productId);
        Task UpdateCartItemQuantity(Guid userId, Guid productId, int quantity);
        Task<Cart> GetCartByUserId(Guid userId);
    }
}
