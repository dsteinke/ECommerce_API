using AutoMapper;
using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task AddItemToCart(Guid userId, Guid productId, int quantity)
        {
            await _cartRepository.AddItemToCart(userId, productId, quantity);
        }

        public decimal CalculateTotal(Cart cart)
        {
            decimal total = 0;

            if(cart.CartItems.Count > 0)
            {
                total = cart.CartItems.Sum(item => item.Product.Price * item.Quantity);

                return total;
            }

            return total;
        }

        public async Task<CartResponseDTO> GetCartByUserId(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);

            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            var total = CalculateTotal(cart);

            var response = _mapper.Map<CartResponseDTO>(cart);
            
            response.TotalPrice = total;

            return response;
        }

        public async Task RemoveItemFromCart(Guid userId, Guid productId)
        {
            await _cartRepository.RemoveItemFromCart(userId, productId);
        }

        public async Task UpdateCartItemQuantity(Guid userId, Guid productId, int quantity)
        {
            await _cartRepository.UpdateCartItemQuantity(userId, productId, quantity);
        }
    }
}
