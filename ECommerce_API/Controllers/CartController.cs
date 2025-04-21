using ECommerce.Application.DTO.Cart;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
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
        /// Gets the cart of logged in User
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(CartResponseDTO), 200)]
        public async Task<IActionResult> GetCartFromUser()
        {
            var cart = await _cartService.GetCartFromUser();

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
                .AddItemToCart(cartAddDTO.ProductId, cartAddDTO.Quantity);

            return Ok(new { message = "Item successfully added to the cart" });
        } 

        /// <summary>
        /// Changes the quantity of item in cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("change-quantity")]
        public async Task<IActionResult> ChangeQuantityOfItem
            ([FromQuery] Guid productId, [FromQuery] int quantity)
        {
            await _cartService.UpdateCartItemQuantity(productId, quantity);

            return Ok(new { message = "Quantity of item changed" });
        }

        /// <summary>
        /// Removes item from cart
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("remove-item/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart([FromRoute] Guid productId)
        {
            await _cartService.RemoveItemFromCart(productId);

            return Ok(new { message = "Item removed successfully" });
        }
    }
}
