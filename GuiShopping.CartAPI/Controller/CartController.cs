using GuiShopping.CartAPI.Data.ValueObject;
using GuiShopping.CartAPI.Model;
using GuiShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuiShopping.CartAPI.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository?? throw new 
                ArgumentException(nameof (repository));
        }

        [HttpGet("find-cart/{id}")]
        public async Task <ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _repository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        
        [HttpPost("add-cart")]
        public async Task <ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCar(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPut("update-cart")]
        public async Task <ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCar(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpDelete("remove-cart/{id}")]
        public async Task <ActionResult<CartVO>> RemoveCart(long id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status) return NotFound();
            return Ok(status);
        }

        
    }
}
