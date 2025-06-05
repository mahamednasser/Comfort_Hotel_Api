using Comfort_Hotel_Api.Data;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        
        public CartItemRepository(AppDBContext _db) : base(_db)
        {
            

        }

        
    }
}
