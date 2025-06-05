using Comfort_Hotel_Api.Data;
using Comfort_Hotel_Api.Models;
using Comfort_Hotel_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comfort_Hotel_Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDBContext _db;
        internal DbSet<T> dbSet;
        public Repository(AppDBContext db)
        {
            _db = db;
            dbSet=_db.Set<T>();
        }
        public async Task CreateAsync(T Entity)
        {
            await dbSet.AddAsync(Entity);

        }

       


        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProprties = null, int pagesize = 0, int pagenum = 1)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            if (pagesize > 0)
            {
                if (pagesize > 100)
                {
                    pagesize = 100;
                }
                query = query.Skip(pagesize * (pagenum - 1)).Take(pagesize);

            }

            if (includeProprties != null)
            {
                foreach (var item in includeProprties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return await query.ToListAsync();
        }



        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string? includeProprties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            if (includeProprties != null)
            {
                foreach (var item in includeProprties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T Entity)
        {
            dbSet.Remove(Entity);
        }
      

        public async Task saveAsync()
        {
            await _db.SaveChangesAsync();
        }

       
    }
}
