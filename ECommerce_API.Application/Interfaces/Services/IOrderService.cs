using ECommerce.Application.DTO.Order;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrder(Guid userId);
        Task<OrderResponseDTO> GetOrderById(Guid orderId);
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserId(Guid userId);
        Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus status);
    }
}
