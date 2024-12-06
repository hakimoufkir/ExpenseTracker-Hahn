using AutoMapper;
using webAPI.DTOs;
using webAPI.enums;
using webAPI.Models;
using webAPI.Repositories.Interfaces;
using webAPI.Services.interfaces;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepo;
    private readonly IExpenseRepository _expenseRepo;
    private readonly IMapper _mapper;

    public BudgetService(IBudgetRepository budgetRepo, IExpenseRepository expenseRepo, IMapper mapper)
    {
        _budgetRepo = budgetRepo;
        _expenseRepo = expenseRepo;
        _mapper = mapper;
    }

    public async Task<BudgetDTO?> GetBudgetAsync(int userId, MonthEnum month)
    {
        var budget = await _budgetRepo.GetAsNoTracking(b => b.UserId == userId && b.Month == month);
        if (budget == null) return null;

        // Dynamically calculate total spent for the month
        var totalSpent = (await _expenseRepo.GetAllAsNoTracking(e => e.UserId == userId && e.Month == month))
                         .Sum(e => e.Amount);

        var budgetDto = _mapper.Map<BudgetDTO>(budget);
        budgetDto.TotalSpent = totalSpent; // Add total spent dynamically

        return budgetDto;
    }

    public async Task<bool> SetMonthlyBudgetAsync(int userId, BudgetDTO budgetDto)
    {
        var budget = await _budgetRepo.GetAsTracking(b => b.UserId == userId && b.Month == budgetDto.Month);

        if (budget == null)
        {
            budget = _mapper.Map<Budget>(budgetDto);
            budget.UserId = userId;
            await _budgetRepo.CreateAsync(budget);
        }
        else
        {
            budget.MonthlyLimit = budgetDto.MonthlyLimit;
            _budgetRepo.UpdateAsync(budget);
        }

        return await _budgetRepo.SaveChangesAsync();
    }

    public async Task<bool> NotifyIfBudgetExceededAsync(int userId, MonthEnum month)
    {
        var budget = await _budgetRepo.GetAsNoTracking(b => b.UserId == userId && b.Month == month);
        if (budget == null) return false;

        var totalExpenses = (await _expenseRepo.GetAllAsNoTracking(e => e.UserId == userId && e.Month == month))
                            .Sum(e => e.Amount);

        return totalExpenses > budget.MonthlyLimit;
    }
}
