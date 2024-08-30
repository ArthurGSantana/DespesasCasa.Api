using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model;
using DespesasCasa.Domain.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DespesasCasa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _authService) : ControllerBase
{
    /// <summary>
    /// Authenticate User
    /// </summary>
    /// <param name="email">User e-mail address</param>
    /// <param name="password">User password</param>
    /// <returns>JWS Authentication Token</returns>
    /// <response code="200">JWS Authentication Token</response>
    /// <response code="400">User Validation Error</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var authDto = await _authService.LoginAsync(loginDto);

        return Ok(new ResponseViewModel<AuthDto>(authDto!));
    }
}
