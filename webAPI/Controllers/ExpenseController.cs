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
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet("{month}")]
    public async Task<IActionResult> GetExpensesByMonth(MonthEnum month)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var expenses = await _expenseService.GetExpensesByMonthAsync(userId, month);
            return Ok(expenses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] ExpenseDTO expenseDto)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var success = await _expenseService.AddExpenseAsync(userId, expenseDto);
            return success ? Ok(new Response(true, "Expense added successfully.")) : BadRequest(new Response(false, "Failed to add expense."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
        }
    }

    [HttpDelete("{expenseId}")]
    public async Task<IActionResult> DeleteExpense(int expenseId)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var success = await _expenseService.DeleteExpenseAsync(expenseId, userId);
            return success ? Ok(new Response(true, "Expense deleted successfully.")) : NotFound(new Response(false, "Expense not found."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
        }
    }

    [HttpGet("{month}/summary-by-category")]
    public async Task<IActionResult> GetExpenseSummaryByCategory(MonthEnum month)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var summary = await _expenseService.GetExpenseSummaryByCategoryAsync(userId, month);
            return Ok(summary);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
        }
    }
}
