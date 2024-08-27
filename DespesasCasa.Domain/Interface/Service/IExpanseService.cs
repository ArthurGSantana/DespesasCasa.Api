using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Domain.Interface.Service;

public interface IExpanseService
{
    Task<ExpanseDto> GetExpanseAsync(Guid id);
    Task<List<ExpanseDto>> GetAllAsync();
    Task<Guid> CreateAsync(ExpanseDto expanse);
    Task<bool> UpdateAsync(ExpanseDto expanse);
    Task<bool> DeleteAsync(Guid id);
}
