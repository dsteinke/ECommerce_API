using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTO.Cart
{
    public class CartItemAddDTO
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
