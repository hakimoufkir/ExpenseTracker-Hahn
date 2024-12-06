using webAPI.Models;

namespace webAPI.Repositories.Interfaces
{
    public interface IBudgetRepository : IGenericRepository<Budget> 
    {
        Task<List<Budget>> GetBudgetsWithUserDetailsAsync(int userId);

    }

}
