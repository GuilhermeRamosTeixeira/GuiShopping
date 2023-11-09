namespace GuiShopping.CartAPI.Model
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetail> CartDetail { get; set; }
    }
}
