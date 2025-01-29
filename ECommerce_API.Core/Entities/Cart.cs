namespace ECommerce_API.Core
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }


        //Navigation Properties
        public User User { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
