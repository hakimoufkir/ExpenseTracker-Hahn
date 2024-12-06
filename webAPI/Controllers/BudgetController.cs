using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webAPI.DTOs;
using webAPI.enums;
using webAPI.Models;
using webAPI.Services.interfaces;

namespace webAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("{month}")]
        [ProducesResponseType(typeof(BudgetDTO), 200)]
        [ProducesResponseType(typeof(Response), 404)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> GetBudget(MonthEnum month)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var budget = await _budgetService.GetBudgetAsync(userId, month);
                return budget != null ? Ok(budget) : NotFound(new Response(false, "Budget not found."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> SetBudget([FromBody] BudgetDTO budgetDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var success = await _budgetService.SetMonthlyBudgetAsync(userId, budgetDto);
                return success ? Ok(new Response(true, "Budget set successfully.")) : BadRequest(new Response(false, "Failed to set budget."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        [HttpGet("{month}/budget-exceeded")]
        public async Task<IActionResult> NotifyIfBudgetExceeded(MonthEnum month)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var isExceeded = await _budgetService.NotifyIfBudgetExceededAsync(userId, month);
                return Ok(new { BudgetExceeded = isExceeded });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }
    }
}
