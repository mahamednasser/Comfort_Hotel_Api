using AutoMapper;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Comfort_Hotel_Api.Controllers
{
    [Route("api/Rates")]
    [ApiController]
    public class RoomRateController : ControllerBase
    {
       private readonly IRoomRateRepository _roomRateRepository;
        private readonly IMapper _mapper;
        public RoomRateController(IRoomRateRepository roomRateRepository,IMapper mapper)
        {
            _roomRateRepository = roomRateRepository;
            _mapper = mapper;
        }

        [HttpGet("{roomid:int}")]
        [ResponseCache(Duration = 30)]

        public async Task<ActionResult<IEnumerable<RoomRateDto>>> GetAll(int roomid,int sizepage,int pagenumber)
        {
            if (roomid == 0)
            {
                return BadRequest("the room is not exist");
            }
            List<RoomsRate> rates = await _roomRateRepository.GetAllAsync(filter: x => x.RoomId == roomid, pagesize: sizepage, pagenum: pagenumber);

            if(rates.Count == 0)
            {
                return NotFound("NoRates");
            }


            return Ok(_mapper.Map<List<RoomRateDto>>(rates));


        }



        [HttpGet("userRate")]
        [ResponseCache(Duration = 30)]
        [Authorize]

        public async Task<ActionResult<IEnumerable<RoomRateDto>>> GetAll()
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

            List<RoomsRate> rates = await _roomRateRepository.GetAllAsync(x => x.UserId== userid);

            if (rates.Count == 0)
            {
                return NotFound("NoRates");
            }


            return Ok(_mapper.Map<List<RoomRateDto>>(rates));


        }



        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddRate([FromBody]RoomRateCreateDto rate)
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
            RoomsRate newRate = _mapper.Map<RoomsRate>(rate);
            newRate.UserId = userid;
            newRate.CreationDate= DateTime.Now;
            await _roomRateRepository.CreateAsync(newRate);
            await _roomRateRepository.saveAsync();
            return Created();
        }


        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateRate (int id,[FromBody]RoomRateDto rate)
        {
            if (id !=rate.Id || id==0 || rate.Id==0)
            {
                return BadRequest();
            }
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

            RoomsRate oldrate=await _roomRateRepository.GetAsync(x=>x.Id == id);
            if (oldrate == null) {

                return BadRequest();
            }
            if (userid == oldrate.UserId)
            {
                RoomsRate updatedRate = _mapper.Map<RoomsRate>(rate);
                await _roomRateRepository.UpdateAsync(updatedRate);
                await _roomRateRepository.saveAsync();
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult> deleteRate(int id)
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
            var rate=await _roomRateRepository.GetAsync(x=>x.Id==id);
            if (userid == rate.UserId)
            {
                await _roomRateRepository.RemoveAsync(rate);
                await _roomRateRepository.saveAsync();
                return NoContent();
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
