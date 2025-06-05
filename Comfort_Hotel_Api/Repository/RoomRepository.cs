using Comfort_Hotel_Api.Data;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository
{
    public class RoomRepository :Repository<Room>, IRoomRepository
    {
        private readonly AppDBContext db;
        public RoomRepository( AppDBContext _db):base( _db ) 
        {
            db= _db;
            
        }

        public async Task UpdateAsync(Room room)
        {
          room.updateDate = DateTime.Now;
          db.Rooms.Update(room);
          
        }
    }
}
