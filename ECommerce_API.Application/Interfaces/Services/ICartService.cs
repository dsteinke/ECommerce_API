using ECommerce.Application.DTO.Cart;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services
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
