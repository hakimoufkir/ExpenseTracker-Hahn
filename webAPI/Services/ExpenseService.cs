using webAPI.DTOs;
using webAPI.Models;
using webAPI.Repositories.Interfaces;
using AutoMapper;
using webAPI.enums;
using webAPI.Services.interfaces;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepo;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepo, IMapper mapper)
    {
        _expenseRepo = expenseRepo;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all expenses for a specific user and month.
    /// </summary>
    public async Task<List<ExpenseDTO>> GetExpensesByMonthAsync(int userId, MonthEnum month)
    {
        var expenses = await _expenseRepo.GetAllAsNoTracking(e => e.UserId == userId && e.Month == month);
        return _mapper.Map<List<ExpenseDTO>>(expenses);
    }

    /// <summary>
    /// Add a new expense for a specific user.
    /// </summary>
    public async Task<bool> AddExpenseAsync(int userId, ExpenseDTO expenseDto)
    {
        var expense = _mapper.Map<Expense>(expenseDto);
        expense.UserId = userId;
        await _expenseRepo.CreateAsync(expense);
        return await _expenseRepo.SaveChangesAsync();
    }

    /// <summary>
    /// Delete an expense by ID for a specific user.
    /// </summary>
    public async Task<bool> DeleteExpenseAsync(int expenseId, int userId)
    {
        var expense = await _expenseRepo.GetAsTracking(e => e.Id == expenseId && e.UserId == userId);
        if (expense == null) return false;

        await _expenseRepo.RemoveAsync(expense);
        return await _expenseRepo.SaveChangesAsync();
    }

    /// <summary>
    /// Get a summary of expenses grouped by category for a specific user and month.
    /// </summary>
    public async Task<List<ExpenseSummaryDTO>> GetExpenseSummaryByCategoryAsync(int userId, MonthEnum month)
    {
        var expenses = await _expenseRepo.GetAllAsNoTracking(e => e.UserId == userId && e.Month == month);
        var summary = expenses
            .GroupBy(e => e.Category)
            .Select(g => new ExpenseSummaryDTO
            {
                Category = g.Key.ToString(),
                TotalAmount = g.Sum(e => e.Amount)
            })
            .ToList();

        return summary;
    }
}
