namespace Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }


        public List<CartItem>? CartItems { get; set; }
    }
}
