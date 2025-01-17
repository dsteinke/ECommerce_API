namespace Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }


        //Navigation Properties
        public User User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
