namespace ECommerce_API.Core
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; } //Fremdschlüssel auf Cart-Tabelle
        public Guid ProductId { get; set; } //Fremdschlüssel auf Product-Tabelle
        public int Quantity { get; set; }


        //Navigation Properties
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
