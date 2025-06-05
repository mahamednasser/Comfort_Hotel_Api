using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository.IRepository
{
    public interface IUserRepository:IRepository<LocalUser>
    {
        
        bool IsUniqueUser(string username);
        Task<TokenDto>Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
