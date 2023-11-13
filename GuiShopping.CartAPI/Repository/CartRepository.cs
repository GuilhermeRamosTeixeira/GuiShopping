using AutoMapper;
using GuiShopping.CartAPI.Data.ValueObject;
using GuiShopping.CartAPI.Model;
using GuiShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GuiShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySQLContext _context;
        private  IMapper _mapper;

        public CartRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string UserId)
        {
            var cartHeader = await _context.cartHeaders
                .FirstOrDefaultAsync(c => c.userId == UserId);

            if(cartHeader == null)
            {
                _context.CartDetails
                    .RemoveRange(
                    _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));
                _context.cartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string UserId)
        {
            Cart cart = new()
            {
            CartHeader  = await _context.cartHeaders
            .FirstOrDefaultAsync(c=>c.userId == UserId),
        };
            cart.CartDetails = _context.CartDetails
                .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                .Include(c => c.Product);
                return _mapper.Map<CartVO>(cart);

        }

        public async Task<bool> RemoveCoupon(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails
                    .FirstOrDefaultAsync(c => c.Id == cartDetailsId);

                int total = _context.CartDetails
                    .Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetail);

                    if(total == 1)
                {
                    var cartHeaderToremove = await _context.cartHeaders
                        .FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                    _context.cartHeaders.Remove(cartHeaderToremove);
                }
                    await _context.SaveChangesAsync();


                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public Task<bool> RemoveFromCart(long cartdetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartVO> SaveOrUpdateCar(CartVO vo)
        {
            Cart cart = _mapper.Map<Cart>(vo);

            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);

            if(product == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            var cartHeader = await _context.cartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c => c.userId == cart.CartHeader.userId);

            if(cartHeader == null)
            {
                _context.cartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
               await _context.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id);
                if(cartDetail == null)
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId= cartDetail.CartHeaderId;
                    await _context.SaveChangesAsync();

                }
            }
            return _mapper.Map<CartVO>(cart);


        }
    }
}
