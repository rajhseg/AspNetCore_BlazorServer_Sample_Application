using ABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abc.BusinessService
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthors();

        Task<Author> UpdateAuthor(Author book);

        Task<IEnumerable<Author>> GetAuthors(Expression<Func<Author, bool>> predicate);

        Task DeleteAuthor(int id);

        Task AddAuthor(Author book);
    }
}
