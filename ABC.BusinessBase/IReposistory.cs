using ABC.Models;
using System.Linq.Expressions;

namespace ABC.BusinessBase
{
    public interface IReposistory<T> where T : class, TEntity
    {
        Task Add(T obj);

        Task<T> Update(T obj);

        Task<bool> Delete(T obj);

        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<int> Save();

        Task<IEnumerable<T>> GetData(Expression<Func<T, bool>> predicate);

    }
}