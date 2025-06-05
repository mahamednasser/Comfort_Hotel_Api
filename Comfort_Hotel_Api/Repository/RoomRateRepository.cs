using Comfort_Hotel_Api.Data;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository
{
    public class RoomRateRepository : Repository<RoomsRate>, IRoomRateRepository
    {
        private readonly AppDBContext db;
        public RoomRateRepository(AppDBContext _db) : base(_db)
        {
            db = _db;

        }

       

        public async Task UpdateAsync(RoomsRate roomRate)
        {
            roomRate.CreationDate = DateTime.Now;
            db.RoomsRates.Update(roomRate);
        }
    }
}
