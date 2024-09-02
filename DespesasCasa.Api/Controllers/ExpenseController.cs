
using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model;
using DespesasCasa.Domain.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DespesasCasa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpenseController(IExpenseService _expenseService) : ControllerBase
{
    /// <summary>
    /// Get Expense
    /// </summary>
    /// <param name="expenseId"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("{expenseId}")]
    public async Task<IActionResult> GetExpenseAsync(Guid expenseId)
    {
        var expense = await _expenseService.GetExpenseAsync(expenseId);
        return Ok(new ResponseViewModel<ExpenseDto>(expense));
    }

    /// <summary>
    /// Get Expenses
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var expenses = await _expenseService.GetAllAsync();
        return Ok(new ResponseViewModel<List<ExpenseDto>>(expenses));
    }

    /// <summary>
    /// Create Expense
    /// </summary>
    /// <param name="expense"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(ExpenseDto expense)
    {
        var expenseId = await _expenseService.CreateAsync(expense);
        return Ok(new ResponseViewModel<Guid>(expenseId));
    }

    /// <summary>
    /// Update Expense
    /// </summary>
    /// <param name="expense"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(ExpenseDto expense)
    {
        var result = await _expenseService.UpdateAsync(expense);
        return Ok(new ResponseViewModel<bool>(result));
    }

    /// <summary>
    /// Delete Expense
    /// </summary>
    /// <param name="expenseId"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{expenseId}")]
    public async Task<IActionResult> DeleteAsync(Guid expenseId)
    {
        var result = await _expenseService.DeleteAsync(expenseId);
        return Ok(new ResponseViewModel<bool>(result));
    }
}
