using GuiShopping.CartAPI.Data.ValueObject;

namespace GuiShopping.CartAPI.Repository
{
    public  interface ICartRepository
    {
        Task<CartVO> FindCartByUserId(string UserId);   
        Task<CartVO> SaveOrUpdateCar(CartVO cart);   
        Task<bool> RemoveFromCart(long cartdetailsId);   
        Task<bool> ApplyCoupon(string userId , string couponCode);   
        Task<bool> RemoveCoupon(long userId);   
        Task<bool> ClearCart(string UserId);   
    }
}
