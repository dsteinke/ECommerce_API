using ECommerce_API.Core.Identity;

namespace ECommerce_API.Core
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public decimal TotalAmount {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        //Navigation Properties
        public ApplicationUser User { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
