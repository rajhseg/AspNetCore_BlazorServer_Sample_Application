using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ABC.Entities.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        
        void Rollback();

        IReposistory<T> Repository<T>() where T : class, TEntity, new();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> CommitTransactionAsync(IDbContextTransaction transaction);

        Task RollbackTransactionAsync(IDbContextTransaction transaction);

    }
}