using ECommerce_API.Core.Enums;

namespace ECommerce_API.Application.DTO.Order
{
    public class OrderResponseDTO
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }
}
