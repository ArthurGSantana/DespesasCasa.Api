using AutoMapper;
using DespesasCasa.Domain.Interface.Repository;
using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Application.Service;

public class ExpenseService(IUnitOfWork _unitOfWork, IMapper _mapper) : IExpenseService
{
    public async Task<ExpenseDto> GetExpenseAsync(Guid id)
    {
        var expense = await _unitOfWork.ExpenseRepository.GetAsync(false, x => x.Id == id);

        if (expense == null)
        {
            throw new Exception("Expense not found");
        }

        return _mapper.Map<ExpenseDto>(expense);
    }

    public async Task<List<ExpenseDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(ExpenseDto expense)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(ExpenseDto expense)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

}
