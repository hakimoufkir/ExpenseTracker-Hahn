using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Models;
using webAPI.Repositories.Interfaces;

namespace webAPI.Repositories
{
    public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Budget>> GetBudgetsWithUserDetailsAsync(int userId)
        {
            return await _db.Budgets
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}
