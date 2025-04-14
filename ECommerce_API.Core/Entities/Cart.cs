using ECommerce.Domain.Identity;

namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }


        //Navigation Properties
        public ApplicationUser User { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
