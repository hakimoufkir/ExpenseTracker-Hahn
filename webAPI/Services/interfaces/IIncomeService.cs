using webAPI.DTOs;
using webAPI.enums;

namespace webAPI.Services.interfaces
{
    public interface IIncomeService
    {
        Task<List<IncomeDTO>> GetIncomesByMonthAsync(int userId, MonthEnum month);
        Task<bool> AddIncomeAsync(int userId, IncomeDTO incomeDto);
        Task<decimal> GetTotalIncomeByMonthAsync(int userId, MonthEnum month);
        Task<bool> DeleteIncomeAsync(int incomeId, int userId);

    }
}
