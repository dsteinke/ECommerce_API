using ECommerce_API.Application;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId([FromRoute] Guid userId)
        {
            var cart = await _cartService.GetCartByUserId(userId);

            return Ok(cart);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart([FromBody] CartItemAddDTO cartAddDTO)
        {
            await _cartService
                .AddItemToCart(cartAddDTO.UserId, cartAddDTO.ProductId, cartAddDTO.Quantity);

            return Ok();
        }

        [HttpPut("change-quantity")]
        public async Task<IActionResult> ChangeQuantityOfItem
            ([FromQuery] Guid userId, [FromQuery] Guid productId, [FromQuery] int quantity)
        {
            await _cartService.UpdateCartItemQuantity(userId, productId, quantity);

            return Ok();
        }

        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveItemFromCart
            ([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            await _cartService.RemoveItemFromCart(userId, productId);

            return Ok();
        }


    }
}
