using AutoMapper;
using Comfort_Hotel_Api.Data;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Comfort_Hotel_Api.Repository
{
    public class UserRepository :Repository<LocalUser>, IUserRepository
    {
        private readonly AppDBContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private String secretkey;
        public UserRepository( AppDBContext _db , IConfiguration _config,UserManager<ApplicationUser> userManager,IMapper mapper, RoleManager<IdentityRole> roleManager) :base( _db ) 
        {
            db= _db;
            secretkey= _config.GetValue<string>("ApiSetting:SecretKey");
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public bool IsUniqueUser(string username)
        {
          var user=db.ApplicationUsers.FirstOrDefault(x=>x.UserName==username);
            return user==null? true : false;
        }

        public async Task<TokenDto> Login(LoginRequestDTO loginRequestDTO)
        {
            var user=db.ApplicationUsers.FirstOrDefault(x=>x.UserName.ToLower()==loginRequestDTO.Username.ToLower());
            if (user == null) {
                return new TokenDto()
                {
                    Token = ""
                   
                };
            
            }
            bool passCheck=await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (passCheck)
            {
                var tokenHandeler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretkey);
                var roles = await _userManager.GetRolesAsync(user);
                var TokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                       
                        new Claim(ClaimTypes.Role,roles.FirstOrDefault())

                    }),
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                LocalUser local = _mapper.Map<LocalUser>(user);
                var token = tokenHandeler.CreateToken(TokenDescripter);
                TokenDto tokenDto = new TokenDto()
                {
                    Token = tokenHandeler.WriteToken(token)
                   
                };
                return tokenDto;
            }
            return new TokenDto()
            {
                Token = "",
               
            };
        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registerationRequestDTO.Name,
                Email = registerationRequestDTO.Email,
                NormalizedEmail=registerationRequestDTO.Email.ToUpper(),
               
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDTO.Passwod);
                if (result.Succeeded)
                {
                    if (! _roleManager.RoleExistsAsync(registerationRequestDTO.Role).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registerationRequestDTO.Role));

                    }
                    await _userManager.AddToRoleAsync(user, registerationRequestDTO.Role);

                    var returnuser = db.ApplicationUsers.FirstOrDefault(x => x.UserName == registerationRequestDTO.Name );
                    return _mapper.Map<LocalUser>(returnuser);



                }
                else
                {
                    return new LocalUser();
                }
            }
            catch (Exception ex)
            {

            }
            return new LocalUser();
           
        }
    }
}
