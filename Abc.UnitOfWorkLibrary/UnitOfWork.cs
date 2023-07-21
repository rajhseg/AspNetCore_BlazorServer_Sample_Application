using Abc.AuthorLibrary;
using ABC.BooksLibrary;
using ABC.BusinessBase;
using ABC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Abc.UnitOfWorkLibrary
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DbContext _context;
        private bool _disposed;       

        public UnitOfWork(DbContext context)
        {
            this._context = context;          
        }

        public async Task<int> CommitAsync()
        {
            var result = await this._context.SaveChangesAsync();                        
            return result;
        }

        public async Task<int> CommitTransactionAsync(IDbContextTransaction transaction)
        {
            var result = await this._context.SaveChangesAsync();

            if (transaction != null)
            {
                await transaction.CommitAsync();
            }

            return result;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            IDbContextTransaction _transaction = await this._context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public void Rollback()
        {
            RevertEntitiesChanges();
        }

        public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            RevertEntitiesChanges();

            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
        }
       
        public IReposistory<T> Repository<T>() where T: class, TEntity, new()
        {
            return new Repository<T>(this._context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();                    
                }
            }

            this._disposed = true;
        }

        private void RevertEntitiesChanges()
        {
            var chanaged = this._context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var item in chanaged)
            {
                switch (item.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        item.CurrentValues.SetValues(item.OriginalValues);
                        item.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        item.State = EntityState.Detached;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
