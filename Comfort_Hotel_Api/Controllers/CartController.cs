using AutoMapper;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Comfort_Hotel_Api.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        public CartController(ICartItemRepository cartItemRepository,IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]


        public async Task<ActionResult<IEnumerable<CartResponseDto>>> GetAll() {
            string userid;
            try
            {
                ClaimsIdentity claims = (ClaimsIdentity)User.Identity;
                 userid = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch
            {
                return BadRequest("You should login");
            }
            if (userid == null ) 
            {
                return BadRequest();
            }

            IEnumerable<CartItem> carts = await _cartItemRepository.GetAllAsync(x => x.UserId == userid);
            return Ok(_mapper.Map<IEnumerable<CartResponseDto>>(carts));
            


        }


        [HttpPost]
        [Authorize]

        public async Task<ActionResult>Addcart([FromBody]CreatecartDto cart)
        {
            string userid;
            try
            {
                ClaimsIdentity claims = (ClaimsIdentity)User.Identity;
                 userid = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch
            {
                return BadRequest("You should login");
            }
            if (userid == null) 
            {
                return BadRequest();
            
            }
            if (cart == null) 
            {
                return BadRequest();
            }
            CartItem newCart = _mapper.Map<CartItem>(cart);
            newCart.AddingDate=DateTime.Now;
            newCart.UserId=userid;
            
           await _cartItemRepository.CreateAsync(newCart);
            await _cartItemRepository.saveAsync();
            return Created();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteCart(int id) { 


            var cart = await _cartItemRepository.GetAsync(x=>x.Id == id);
            if (cart == null)
            {
                return NotFound();
            }
            await _cartItemRepository.RemoveAsync(cart);
            await _cartItemRepository.saveAsync();
            return NoContent();
        }

    }
}
