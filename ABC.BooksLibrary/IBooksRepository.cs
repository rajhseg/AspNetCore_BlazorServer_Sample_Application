using ABC.BusinessBase;
using ABC.Models;

namespace ABC.BooksLibrary
{
    public interface IBooksRepository : IReposistory<Book>
    {
        Task updateDescriptionAsync(int Id, string description);

        Task<IEnumerable<Book>> GetAllWithAuthors();
    }
}