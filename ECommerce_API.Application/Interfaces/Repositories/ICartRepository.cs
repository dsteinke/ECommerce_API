using ECommerce.Domain.Entities;
using ECommerce.Domain.Identity;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> AddCartToUser(ApplicationUser user);
        Task<int> AddItemToCart(CartItem cartItem);
        Task<int> UpdateCartItem(CartItem item);
        Task<int> RemoveItemFromCart(CartItem item);
        Task<int> UpdateCartItemQuantity(Guid userId, Guid productId, int quantity);
        Task<Cart?> GetCartByUserId(Guid userId);
        Task ClearCart(Guid userId);
    }
}
