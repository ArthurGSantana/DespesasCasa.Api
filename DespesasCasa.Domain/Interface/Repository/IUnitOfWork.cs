using DespesasCasa.Domain.Entity;

namespace DespesasCasa.Domain.Interface.Repository;

public interface IUnitOfWork
{
    IBaseRepository<User> UserRepository { get; }
    IBaseRepository<Collection> CollectionRepository { get; }
    IBaseRepository<Expense> ExpenseRepository { get; }

    Task CommitAsync();
}
