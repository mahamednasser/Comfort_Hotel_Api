using Comfort_Hotel_Api.Models;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository.IRepository
{
    public interface IRoomRepository:IRepository<Room>
    {
        Task UpdateAsync(Room room);

    }
}
