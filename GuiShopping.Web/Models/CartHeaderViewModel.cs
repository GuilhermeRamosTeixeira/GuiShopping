

namespace GuiShopping.Web.Models
{
    public class CartHeaderViewModel
    {
        public long Id { get; set; }
        public string userId { get; set; }
        public string CouponCode { get; set; }
        public double PurchaseAmount { get; set; }
    }
}
