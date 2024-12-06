using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Models;
using webAPI.Repositories.Interfaces;

namespace webAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllUsersWithDetailsAsync()
        {
            return await _db.Users
                .Include(u => u.Expenses)
                .Include(u => u.Budget)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
