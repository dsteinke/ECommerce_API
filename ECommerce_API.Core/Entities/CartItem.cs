namespace ECommerce.Domain.Entities
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }


        //Navigation Properties
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
