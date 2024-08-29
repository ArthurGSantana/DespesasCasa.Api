using AutoMapper;
using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Enum;
using DespesasCasa.Domain.Exceptions;
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
            throw new AppDomainException("Expense not found", ErrorCodeEnum.ObjectNotFound);
        }

        return _mapper.Map<ExpenseDto>(expense);
    }

    public async Task<List<ExpenseDto>> GetAllAsync()
    {
        return _mapper.Map<List<ExpenseDto>>(await _unitOfWork.ExpenseRepository.GetAllAsync());
    }

    public async Task<Guid> CreateAsync(ExpenseDto expense)
    {
        var existing = await _unitOfWork.ExpenseRepository.FindAsync(x => x.Id == expense.Id);

        if (existing)
        {
            throw new AppDomainException("Expense already exists", ErrorCodeEnum.ObjectAlreadyExists);
        }

        var newExpense = _mapper.Map<Expense>(expense);

        await _unitOfWork.ExpenseRepository.AddAsync(newExpense);
        await _unitOfWork.CommitAsync();

        return newExpense.Id;
    }

    public async Task<bool> UpdateAsync(ExpenseDto expense)
    {
        var existing = await _unitOfWork.ExpenseRepository.FindAsync(x => x.Id == expense.Id);

        if (!existing)
        {
            throw new AppDomainException("Expense not found", ErrorCodeEnum.ObjectNotFound);
        }

        var updatedExpense = _mapper.Map<Expense>(expense);

        _unitOfWork.ExpenseRepository.Update(updatedExpense);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var expense = await _unitOfWork.ExpenseRepository.GetAsync(false, x => x.Id == id);

        if (expense is null)
        {
            throw new AppDomainException("Expense not found", ErrorCodeEnum.ObjectNotFound);
        }

        _unitOfWork.ExpenseRepository.Remove(expense);
        await _unitOfWork.CommitAsync();

        return true;
    }

}
