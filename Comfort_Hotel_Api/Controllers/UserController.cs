using Comfort_Hotel_Api.Models.DTO;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comfort_Hotel_Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Login")]

        public async Task<ActionResult> Login( [FromBody]LoginRequestDTO Model)
        {
            var tokenDto=await _userRepository.Login(Model);
            if (tokenDto== null || String.IsNullOrEmpty(tokenDto.Token) ){ 
                return BadRequest("the Username or Password Is incorrect");
            }
            return Ok(tokenDto);
        }

        [HttpPost("Register")]

        public async Task<ActionResult> Register([FromBody] RegisterationRequestDTO Model)
        {
            bool ifunique=_userRepository.IsUniqueUser(Model.Name);
            if (ifunique == false)
            {
                return BadRequest("Username Is already exist");
            }

            var user=await _userRepository.Register(Model);
            if (user.UserName == null|| user.Email == null) {
                return BadRequest("error while register My be the password should have at least : one nonalpha,one digit , one uppercase");
            }
            return Ok();
        }

    }
}
