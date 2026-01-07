
using ABC.Entities;
using ABC.Entities.Interfaces;
using System.Linq.Expressions;

namespace ABC.Entities.Interfaces
{
    public interface IBooksRepository : IReposistory<Book>
    {
        Task updateDescriptionAsync(int Id, string description);

        Task<IEnumerable<Book>> GetAllWithAuthors();

        Task<IEnumerable<Book>> GetAllWithAuthors(Expression<Func<Book, bool>> predicate);
    }
}