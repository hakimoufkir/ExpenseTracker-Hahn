using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webAPI.DTOs;
using webAPI.enums;
using webAPI.Models;
using webAPI.Services.interfaces;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet("{month}")]
    public async Task<IActionResult> GetIncomesByMonth(MonthEnum month)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var incomes = await _incomeService.GetIncomesByMonthAsync(userId, month);
            return Ok(incomes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddIncome([FromBody] IncomeDTO incomeDto)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var success = await _incomeService.AddIncomeAsync(userId, incomeDto);
            return success ? Ok(new { message = "Income added successfully." }) : BadRequest(new { message = "Failed to add income." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
        }
    }

    [HttpGet("{month}/total")]
    public async Task<IActionResult> GetTotalIncomeByMonth(MonthEnum month)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var totalIncome = await _incomeService.GetTotalIncomeByMonthAsync(userId, month);
            return Ok(new { TotalIncome = totalIncome });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
        }
    }

    [HttpDelete("{incomeId}")]
    public async Task<IActionResult> DeleteIncome(int incomeId)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var success = await _incomeService.DeleteIncomeAsync(incomeId, userId);
            return success ? Ok(new { message = "Income deleted successfully." }) : NotFound(new { message = "Income not found." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
        }
    }
}
