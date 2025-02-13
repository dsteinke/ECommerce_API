using ECommerce_API.Application.Interfaces;
using ECommerce_API.Core;
using ECommerce_API.Core.Enums;

namespace ECommerce_API.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(Guid userId, List<OrderItem> items)
        {
            var total = items.Sum(x => x.Quantity *  x.Price);

            var order = new Order
            {
                UserId = userId,
                Items = items,
                TotalAmount = total
            };

            await _orderRepository.CreateOrder(order);

            return order;
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
                throw new KeyNotFoundException($"No order with orderId: {orderId} found.");

            return order;
        }

        public async Task<IEnumerable<Order>?> GetOrdersByUserId(Guid userId)
        {
            return await _orderRepository.GetOrdersByUserId(userId);
        }

        public async Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
