using Comfort_Hotel_Api.Models;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T Entity);
        Task RemoveAsync(T Entity);
        Task saveAsync();
      
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProprties = null, int pagesize = 0, int pagenum = 1);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string? includeProprties = null);
    }
}
