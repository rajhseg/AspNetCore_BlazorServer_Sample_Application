using ABC.BusinessBase;
using ABC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Abc.UnitOfWorkLibrary
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