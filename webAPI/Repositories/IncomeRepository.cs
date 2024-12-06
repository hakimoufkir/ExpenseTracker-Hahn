using webAPI.Data;
using webAPI.Models;
using webAPI.Repositories.Interfaces;

namespace webAPI.Repositories
{
    public class IncomeRepository : GenericRepository<Income>, IIncomeRepository
    {
        public IncomeRepository(ApplicationDbContext db) : base(db) { }
    }
}
