using AutoMapper;
using ECommerce.Application.DTO.Cart;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CartService
            (ICartRepository cartRepository, IProductRepository productRepository, IUserService userService, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<bool> AddItemToCart(Guid productId, int quantity)
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with Id: {productId} not found");

            var cart = await _cartRepository.GetCartByUserId(userId);

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItem(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity,
                };

                await _cartRepository.AddItemToCart(newItem);
            }

            return true;
        }

        private decimal CalculateTotal(Cart cart)
        {
            decimal total = 0;

            if (cart.CartItems.Count > 0)
            {
                total = cart.CartItems.Sum(item => item.Product.Price * item.Quantity);

                return total;
            }

            return total;
        }

        public async Task<CartResponseDTO> GetCartFromUser()
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var cart = await _cartRepository.GetCartByUserId(userId);

            var total = CalculateTotal(cart);

            var response = _mapper.Map<CartResponseDTO>(cart);

            response.TotalPrice = total;

            return response;
        }

        public async Task RemoveItemFromCart(Guid productId)
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with Id {productId} not found.");

            var cart = await _cartRepository.GetCartByUserId(userId);

            var itemToRemove = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (itemToRemove == null)
                throw new KeyNotFoundException($"Product with Id {productId} is not in the cart.");

            await _cartRepository.RemoveItemFromCart(itemToRemove);
        }

        public async Task UpdateCartItemQuantity(Guid productId, int quantity)
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var cart = await _cartRepository.GetCartByUserId(userId);

            var item = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);

            if (item == null)
                throw new KeyNotFoundException($"Product with Id: {productId} not found in cart");

            await _cartRepository.UpdateCartItemQuantity(userId, productId, quantity);
        }
    }
}
