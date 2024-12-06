using webAPI.DTOs;
using webAPI.enums;

namespace webAPI.Services.interfaces
{
    public interface IBudgetService
    {
        Task<BudgetDTO?> GetBudgetAsync(int userId, MonthEnum month);
        Task<bool> SetMonthlyBudgetAsync(int userId, BudgetDTO budgetDto);
        Task<bool> NotifyIfBudgetExceededAsync(int userId, MonthEnum month);
    }
}
