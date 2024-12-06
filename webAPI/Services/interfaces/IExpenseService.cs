using webAPI.DTOs;
using webAPI.enums;

namespace webAPI.Services.interfaces
{
    public interface IExpenseService
    {
        Task<List<ExpenseDTO>> GetExpensesByMonthAsync(int userId, MonthEnum month);
        Task<bool> AddExpenseAsync(int userId, ExpenseDTO expenseDto);
        Task<bool> DeleteExpenseAsync(int expenseId, int userId);
        Task<List<ExpenseSummaryDTO>> GetExpenseSummaryByCategoryAsync(int userId, MonthEnum month);
    }
}
