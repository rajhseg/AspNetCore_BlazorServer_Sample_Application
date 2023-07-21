using ABC.BusinessBase;
using ABC.Models;

namespace Abc.AuthorLibrary
{
    public interface IAuthorRepository : IReposistory<Author>
    {
        Task UpdateAuthorName(int id, string name);

        Task<IEnumerable<Author>> GetAllWithBooks();
    }
}