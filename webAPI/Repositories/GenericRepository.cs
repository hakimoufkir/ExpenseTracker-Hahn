using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using webAPI.Data;
using webAPI.Repositories.Interfaces;

namespace webAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<List<T>> GetAllAsNoTracking(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsNoTracking(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsTracking(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet.AsTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsTracking(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet.AsTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task CreateRangeAsync(ICollection<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
