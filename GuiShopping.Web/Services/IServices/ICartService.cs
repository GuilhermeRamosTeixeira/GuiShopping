using GuiShopping.Web.Models;

namespace GuiShopping.Web.Services.IServices
{
    public interface ICartService
    {
        Task<CartViewModel> FindcartByUserId(string userId, string token);
        Task<CartViewModel> AddItemToCart(CartViewModel cart, string token);
        Task<CartViewModel> UpdateCart(CartViewModel cart, string token);
        Task<bool> RemoveFromCart (long cartId , string token);
        Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token);
        Task<bool> removeCoupon(string userId, string token);
        Task<bool> ClearCart (string userId, string token);

        Task <CartViewModel> Chwckout (CartHeaderViewModel cartheader , string token);

    }
}
