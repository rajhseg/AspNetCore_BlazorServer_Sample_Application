
namespace ABC.Entities.Interfaces
{
    public interface IAuthorRepository : IReposistory<Author>
    {
        Task UpdateAuthorName(int id, string name);

        Task<IEnumerable<Author>> GetAllWithBooks();
    }
}