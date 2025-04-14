using ECommerce_API.Application.DTO.Order;
using ECommerce_API.Core;
using ECommerce_API.Core.Enums;

namespace ECommerce_API.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrder(Guid userId);
        Task<OrderResponseDTO> GetOrderById(Guid orderId);
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserId(Guid userId);
        Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus status);
    }
}
