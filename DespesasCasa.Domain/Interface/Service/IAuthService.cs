using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Domain.Interface.Service;

public interface IAuthService
{
    Task<AuthDto?> LoginAsync(LoginDto loginDto);
    string GenerateToken(User user);
}
