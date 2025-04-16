using ECommerce.Application.DTO.Order;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Checkout order
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutOrder()
        {
            await _orderService.CreateOrder();

            return Ok(new { message = "Thank you for your order!" });
        }

        /// <summary>
        /// Gets order by orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResponseDTO), 200)]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
        {
            var response = await _orderService.GetOrderById(orderId);

            return Ok(response);
        }

        /// <summary>
        /// Gets all orders of logged in user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("orders")]
        [ProducesResponseType(typeof(List<OrderResponseDTO>), 200)]
        public async Task<IActionResult> GetOrdersFromUser()
        {
            var response = await _orderService.GetOrdersFromUser();

            return Ok(response);
        }

        /// <summary>
        /// Updates the order status
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus
            ([FromQuery] Guid orderId, [FromQuery] OrderStatus orderStatus)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, orderStatus);

            return Ok(new { message = "Order Status successfully updated." });
        }
    }
}
