using ECommerce_API.Core;

namespace ECommerce_API.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<Order?> GetOrderById(Guid orderId);
        Task<IEnumerable<Order>?> GetOrdersByUserId(Guid userId);
        Task<bool> UpdateOrderStatus(Guid orderId, string status);
    }
}
