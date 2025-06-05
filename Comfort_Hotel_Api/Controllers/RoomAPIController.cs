using AutoMapper;
using Comfort_Hotel_Api.Data;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Comfort_Hotel_Api.Controllers
{
    [Route("api/Room")]
    [ApiController]

    public class RoomAPIController:ControllerBase
    {

        private readonly IRoomRepository _dbRoom;
        private readonly IMapper _mapper;

        public RoomAPIController(IRoomRepository dbRoom, IMapper mapper)
        {
            _dbRoom = dbRoom;
            _mapper = mapper;
        }


        [HttpGet]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 30)]

        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoom(int sizepage,int pagenumber)
        {
            IEnumerable<Room> RoomList = await _dbRoom.GetAllAsync(pagesize:sizepage,pagenum:pagenumber);


            return Ok(_mapper.Map<IEnumerable<RoomDTO>>(RoomList));


        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RoomDTO>> GetRoomId(int id) 
        {

            if (id == 0)
            {
               
                return NotFound();

            }
            else
            {
                Room room = await _dbRoom.GetAsync(x => x.Id == id);
                return Ok(_mapper.Map<RoomDTO>(room));
            }
        
    
        
        
        }


        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<RoomDTO>>create( [FromForm]RoomCreateDTO roomDto) 
        {
            if (roomDto == null) {
                return BadRequest();
            
            }
            if(await _dbRoom.GetAsync(x=>x.Name.ToLower() == roomDto.Name.ToLower()) !=null )
            {
                ModelState.AddModelError("Error", "Room Is Already Exist");
                return BadRequest(ModelState);
            }

            Room room = _mapper.Map<Room>(roomDto);
            room.createDate = DateTime.Now;
            
            await _dbRoom.CreateAsync(room);
            await _dbRoom.saveAsync();

            if (roomDto.Image != null)
            {
                string filename =room.Id+ Path.GetExtension(roomDto.Image.FileName);
                string filePath = @"wwwroot\RoomImages\" + filename;
                var directory=Path.Combine(Directory.GetCurrentDirectory(),filePath);
                FileInfo file = new FileInfo(directory);
                if (file.Exists)
                {
                    file.Delete();
                }
                using (var filestream=new FileStream(directory,FileMode.Create))
                {
                    roomDto.Image.CopyTo(filestream);
                    
                }
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                room.ImageUrl = baseUrl +"/RoomImages/" +filename;
                room.ImageLocalPath = filePath;
            }
            else
            {
                room.ImageUrl = "https://placehold.co/600x499";
            }
            await _dbRoom.UpdateAsync(room);
            await _dbRoom.saveAsync();
                return Ok();
            
        }

        [HttpDelete ("{id:int}")]
        [Authorize(Roles ="admin")]

        public async Task<ActionResult> Delete(int id) 
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            var room = await _dbRoom.GetAsync(x => x.Id == id);
            if (room == null) { 
                return NotFound();
            }
            if (!string.IsNullOrEmpty(room.ImageLocalPath))
            {
                var olddiectoy = Path.Combine(Directory.GetCurrentDirectory(), room.ImageLocalPath);
                FileInfo file = new FileInfo(olddiectoy);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            await _dbRoom.RemoveAsync(room);
            await _dbRoom.saveAsync();
            return NoContent();
                   
        
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles ="admin")]

        public async Task<ActionResult> Update (int id, [FromForm] RoomUpdateDTO roomUpdateDto)
          {
            if (roomUpdateDto == null|| id != roomUpdateDto.Id) {

                return BadRequest();
            }
                Room room = _mapper.Map<Room>(roomUpdateDto);
            if (roomUpdateDto.Image != null)
            {
                if (!string.IsNullOrEmpty(room.ImageLocalPath))
                {
                    var olddiectoy=Path.Combine(Directory.GetCurrentDirectory(), room.ImageLocalPath);
                    FileInfo file = new FileInfo(olddiectoy);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                string filename = roomUpdateDto.Id + Path.GetExtension(roomUpdateDto.Image.FileName);
                string filePath = @"wwwroot\RoomImages\" + filename;
                var directory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
               
                using (var filestream = new FileStream(directory, FileMode.Create))
                {
                    roomUpdateDto.Image.CopyTo(filestream);

                }
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                room.ImageUrl = baseUrl + "/RoomImages/" + filename;
                room.ImageLocalPath = filePath;
            }
            else
            {
                room.ImageUrl = "https://placehold.co/600x499";
            }
            await _dbRoom.UpdateAsync(room);
                await _dbRoom.saveAsync();
                return Ok();
            
            return NotFound();
        }


    }



    
}
