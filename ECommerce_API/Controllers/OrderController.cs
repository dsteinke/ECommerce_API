using ECommerce_API.Application.DTO.Order;
using ECommerce_API.Application.Interfaces;
using ECommerce_API.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
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
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> CheckoutOrder([FromRoute] Guid userId)
        {
            await _orderService.CreateOrder(userId);

            return Ok(new { message = "Thank you for your order!" });
        }

        /// <summary>
        /// Gets order by orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResponseDTO), 200)]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
        {
            var response = await _orderService.GetOrderById(orderId);

            return Ok(response);
        }

        /// <summary>
        /// Gets all orders of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("orders/{userId}")]
        [ProducesResponseType(typeof(List<OrderResponseDTO>), 200)]
        public async Task<IActionResult> GetOrdersByUserId([FromRoute] Guid userId)
        {
            var response = await _orderService.GetOrdersByUserId(userId);

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
