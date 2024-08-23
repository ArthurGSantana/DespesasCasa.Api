using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Domain.Interface.Service;

public interface IUserService
{
    Task<UserDto> GetUserAsync(Guid id);
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> CreateAsync(UserDto user);
    Task<bool> DeleteAsync(Guid id);
}
