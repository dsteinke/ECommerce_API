using ECommerce_API.Core;

namespace ECommerce_API.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Guid userId, List<OrderItem> items);
        Task<Order?> GetOrderById(Guid orderId);
        Task<IEnumerable<Order>?> GetOrdersByUserId(Guid userId);
        Task<bool> UpdateOrderStatus(Guid orderId, string status);
    }
}
