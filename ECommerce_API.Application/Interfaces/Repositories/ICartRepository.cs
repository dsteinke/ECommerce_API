using ECommerce.Domain.Entities;
using ECommerce.Domain.Identity;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> AddCartToUser(ApplicationUser user);
        Task AddItemToCart(Guid userId, Guid productId, int quantity);
        Task RemoveItemFromCart(Guid userId, Guid productId);
        Task UpdateCartItemQuantity(Guid userId, Guid productId, int quantity);
        Task<Cart> GetCartByUserId(Guid userId);
        Task ClearCart(Guid userId);
    }
}
