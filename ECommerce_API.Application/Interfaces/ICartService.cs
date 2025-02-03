using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public interface ICartService
    {
        Task AddItemToCart(Guid userId, Guid productId, int quantity);
        Task<CartResponseDTO> GetCartByUserId(Guid userId);
        Task RemoveItemFromCart(Guid userId, Guid productId);
        Task UpdateCartItemQuantity(Guid userId, Guid productId, int quantity);
        decimal CalculateTotal(Cart cart);
    }
}
