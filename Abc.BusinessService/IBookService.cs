using ABC.Models;
using System.Linq.Expressions;

namespace Abc.BusinessService
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();

        Task<Book> UpdateBook(Book book);

        Task DeleteBook(int id);

        Task AddBook(Book book);

        Task<IEnumerable<Book>> GetBooks(Expression<Func<Book, bool>> predicate);
    }
}