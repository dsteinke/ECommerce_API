using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<Order?> GetOrderById(Guid orderId);
        Task<IEnumerable<Order>> GetOrdersByUserId(Guid userId);
        Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus status);
        Task SaveChangesAsync();
    }
}
