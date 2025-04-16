using AutoMapper;
using ECommerce.Application.DTO.Order;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrderService
            (IOrderRepository orderRepository, ICartRepository cartRepository, IUserService userService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<OrderResponseDTO> CreateOrder()
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var cart = await _cartRepository.GetCartByUserId(userId);

            if (!cart.CartItems.Any())
                throw new InvalidOperationException("The cart is empty.");

            var (orderItems, total) = CreateOrderItemsFromCart(cart);

            var order = new Order
            {
                UserId = userId,
                Items = orderItems,
                TotalAmount = total
            };

            await _orderRepository.CreateOrder(order);

            await _cartRepository.ClearCart(userId);

            await _orderRepository.SaveChangesAsync();

            var response = _mapper.Map<OrderResponseDTO>(order);

            return response;
        }

        public async Task<OrderResponseDTO> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
                throw new KeyNotFoundException($"No order with orderId: {orderId} found.");

            var response = _mapper.Map<OrderResponseDTO>(order);

            return response;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersFromUser()
        {
            var userId = _userService.GetUserId_LoggedInUser();

            var orders =  await _orderRepository.GetOrdersByUserId(userId);

            var response = _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);

            return response;
        }

        public async Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if(order == null)
                throw new KeyNotFoundException($"No order with orderId: {orderId} found.");

            order.Status = status;

            await _orderRepository.SaveChangesAsync();
            
            return true;
        }

        private (List<OrderItem> items, decimal total) CreateOrderItemsFromCart(Cart cart)
        {
            var items = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Quantity = ci.Quantity,
                Price = ci.Product.Price
            }).ToList();

            var total = items.Sum(item => item.Quantity * item.Price);

            return (items, total);
        }
    }
}
