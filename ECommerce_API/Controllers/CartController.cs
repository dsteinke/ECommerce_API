using ECommerce_API.Application;
using ECommerce_API.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Gets the cart of user by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(CartResponseDTO), 200)]
        public async Task<IActionResult> GetCartByUserId([FromRoute] Guid userId)
        {
            var cart = await _cartService.GetCartByUserId(userId);

            return Ok(cart);
        }

        /// <summary>
        /// Adds item to cart
        /// </summary>
        /// <param name="cartAddDTO"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart([FromBody] CartItemAddDTO cartAddDTO)
        {
            await _cartService
                .AddItemToCart(cartAddDTO.UserId, cartAddDTO.ProductId, cartAddDTO.Quantity);

            return Ok();
        }

        /// <summary>
        /// Changes the quantity of item in cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("change-quantity")]
        public async Task<IActionResult> ChangeQuantityOfItem
            ([FromQuery] Guid userId, [FromQuery] Guid productId, [FromQuery] int quantity)
        {
            await _cartService.UpdateCartItemQuantity(userId, productId, quantity);

            return Ok();
        }

        /// <summary>
        /// Removes item from cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveItemFromCart
            ([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            await _cartService.RemoveItemFromCart(userId, productId);

            return Ok();
        }
    }
}
