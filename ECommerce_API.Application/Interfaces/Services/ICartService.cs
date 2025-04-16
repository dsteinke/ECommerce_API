using ECommerce.Application.DTO.Cart;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<bool> AddItemToCart(Guid productId, int quantity);
        Task<CartResponseDTO> GetCartFromUser();
        Task RemoveItemFromCart(Guid productId);
        Task UpdateCartItemQuantity(Guid productId, int quantity);
    }
}
