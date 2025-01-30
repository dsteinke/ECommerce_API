using ECommerce_API.Core.Identity;

namespace ECommerce_API.Core
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }


        //Navigation Properties
        public ApplicationUser User { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
