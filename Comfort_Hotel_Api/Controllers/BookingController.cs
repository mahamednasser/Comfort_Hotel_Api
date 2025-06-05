using AutoMapper;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Comfort_Hotel_Api.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _booking;
        private readonly IMapper _mapper;
        public BookingController(IBookingRepository booking,IMapper mapper)
        {
            _booking = booking;
            _mapper=mapper;
        }



        [HttpGet("AllRooms")]
        [Authorize(Roles = "admin")]
        //for cacheing
        [ResponseCache(Duration =30)]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookingforAdmin(int sizepage,int pagenumber)
        {
            return Ok(await _booking.GetAllAsync(pagesize:sizepage,pagenum:pagenumber));
        }


        [HttpGet]
        [ResponseCache(Duration =30)]
        [Authorize]


        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
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
                return BadRequest("You Should Login");
            }

            IEnumerable<Booking> bookings = await _booking.GetAllAsync(x => x.UserId == userid);

            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }


        [HttpPost()]
        [Authorize]
        public async Task<ActionResult> AddBooking(BookingCreateDto Book)
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
                return BadRequest("You Should Login");
            }


            if (Book == null)
            {
                return BadRequest();
            }
            
            Room room = await _booking.FindRoomAsync(Book.RoomId);

            if (room == null )
            {
                return BadRequest();
            }
             bool isAvailable= await _booking.IsRoomAvailable(room.Id,Book.StartDate,Book.EndDate);
            if (isAvailable)
            {
                int days = (Book.EndDate - Book.StartDate).Days;
                Booking newBook = _mapper.Map<Booking>(Book);
                newBook.price = room.PricePerNight * days;
                newBook.creationDate = DateTime.Now;
                newBook.UserId = userid;
                
                await _booking.CreateAsync(newBook);
                await _booking.saveAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Sorry the room is not Available");
            }
           
           


        }
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var booking=await _booking.GetAsync(x=>x.Id==id);
            if ( booking==null)
            {
                return BadRequest();
            }
            await _booking.RemoveAsync(booking);
            await _booking.saveAsync();
            return NoContent();
        }

      
    }
}
