using System.ComponentModel.DataAnnotations;

namespace ECommerce_API.Application
{
    public class CartItemAddDTO
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
