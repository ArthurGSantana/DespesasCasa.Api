using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Domain.Interface.Service;

public interface IExpenseService
{
    Task<ExpenseDto> GetExpenseAsync(Guid id);
    Task<List<ExpenseDto>> GetAllAsync();
    Task<Guid> CreateAsync(ExpenseDto expanse);
    Task<bool> UpdateAsync(ExpenseDto expanse);
    Task<bool> DeleteAsync(Guid id);
}
