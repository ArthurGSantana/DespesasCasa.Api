using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model;
using DespesasCasa.Domain.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DespesasCasa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService _userService) : ControllerBase
{
    /// <summary>
    /// Get User
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAsync(Guid userId)
    {
        var result = await _userService.GetUserAsync(userId);
        return Ok(new ResponseViewModel<UserDto>(result));
    }

    /// <summary>
    /// Get Users
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _userService.GetAllAsync();
        return Ok(new ResponseViewModel<List<UserDto>>(result));
    }

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserDto user)
    {
        var result = await _userService.CreateAsync(user);
        return Ok(new ResponseViewModel<Guid>(result));
    }

    /// <summary>
    /// Remove User
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(Guid userId)
    {
        var result = await _userService.DeleteAsync(userId);
        return Ok(new ResponseViewModel<bool>(result));
    }
}
