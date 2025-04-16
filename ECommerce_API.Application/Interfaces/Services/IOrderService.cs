using ECommerce.Application.DTO.Order;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrder();
        Task<OrderResponseDTO> GetOrderById(Guid orderId);
        Task<IEnumerable<OrderResponseDTO>> GetOrdersFromUser();
        Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus status);
    }
}
