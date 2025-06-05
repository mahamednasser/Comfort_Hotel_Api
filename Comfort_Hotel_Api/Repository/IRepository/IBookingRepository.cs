using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository.IRepository
{
    public interface IBookingRepository:IRepository<Booking>
    {
        Task<Room> FindRoomAsync(int roomId);
        Task<bool> IsRoomAvailable(int roomId ,DateTime startdate , DateTime Enddate);


        
    }
}