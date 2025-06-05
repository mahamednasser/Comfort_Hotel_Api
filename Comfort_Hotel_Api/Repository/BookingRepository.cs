using Comfort_Hotel_Api.Data;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Models.DTO;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository
{
    public class BookingRepository :Repository<Booking>, IBookingRepository
    {
        private readonly AppDBContext db;
        public BookingRepository(AppDBContext _db):base(_db)
        {
            db= _db;
            
        }

        

        public async Task<Room> FindRoomAsync(int roomId)
        {
            Room room= await db.Rooms.FirstOrDefaultAsync(x=>x.Id==roomId);
           
            return room;
        }

       

       

        public async Task<bool> IsRoomAvailable(int roomId, DateTime startdate, DateTime Enddate)
        {
            return !await db.Bookings.AnyAsync(b => b.RoomId == roomId && b.StartDate < Enddate&&  b.EndDate > startdate);
        }

        
    }
}
