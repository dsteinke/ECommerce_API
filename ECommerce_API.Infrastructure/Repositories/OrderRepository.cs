using ECommerce_API.Application.Interfaces;
using ECommerce_API.Core;
using ECommerce_API.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;

        public OrderRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            return await _context.Orders.Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>?> GetOrdersByUserId(Guid userId)
        {
            return await _context.Orders
                .Include(x => x.Items)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);

            order.Status = status;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
