using DespesasCasa.Data.Context;
using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace DespesasCasa.Data.Repository;

public class UnitOfWork(PostgresDbContext context) : IUnitOfWork
{
    private IBaseRepository<User>? _userRepository;
    private IBaseRepository<Collection>? _collectionRepository;
    private IBaseRepository<Expense>? _expenseRepository;

    public IBaseRepository<User> UserRepository => _userRepository ??= new BaseRepository<User>(context);
    public IBaseRepository<Collection> CollectionRepository => _collectionRepository ??= new BaseRepository<Collection>(context);
    public IBaseRepository<Expense> ExpenseRepository => _expenseRepository ??= new BaseRepository<Expense>(context);

    public async Task CommitAsync()
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while saving the changes.", ex);
        }
    }
}
