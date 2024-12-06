using AutoMapper;
using webAPI.DTOs;
using webAPI.Models;
using webAPI.Repositories.Interfaces;
using webAPI.enums;
using webAPI.Services.interfaces;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomeRepo;
    private readonly IMapper _mapper;

    public IncomeService(IIncomeRepository incomeRepo, IMapper mapper)
    {
        _incomeRepo = incomeRepo;
        _mapper = mapper;
    }

    public async Task<List<IncomeDTO>> GetIncomesByMonthAsync(int userId, MonthEnum month)
    {
        var incomes = await _incomeRepo.GetAllAsNoTracking(i => i.UserId == userId && i.Month == month);
        return _mapper.Map<List<IncomeDTO>>(incomes);
    }

    public async Task<bool> AddIncomeAsync(int userId, IncomeDTO incomeDto)
    {
        var income = _mapper.Map<Income>(incomeDto);
        income.UserId = userId;
        await _incomeRepo.CreateAsync(income);
        return await _incomeRepo.SaveChangesAsync();
    }

    public async Task<decimal> GetTotalIncomeByMonthAsync(int userId, MonthEnum month)
    {
        var incomes = await _incomeRepo.GetAllAsNoTracking(i => i.UserId == userId && i.Month == month);
        return incomes.Sum(i => i.Amount);
    }
    public async Task<bool> DeleteIncomeAsync(int incomeId, int userId)
    {
        var income = await _incomeRepo.GetAsTracking(i => i.Id == incomeId && i.UserId == userId);
        if (income == null) return false;

        await _incomeRepo.RemoveAsync(income);
        return await _incomeRepo.SaveChangesAsync();
    }
}
