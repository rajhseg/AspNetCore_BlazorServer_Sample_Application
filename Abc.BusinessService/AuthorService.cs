using Abc.AuthorLibrary;
using ABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abc.BusinessService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository repository)
        {
            this._authorRepository = repository;
        }

        public async Task AddAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException("author");

            await this._authorRepository.Add(author);
        }

        public async Task DeleteAuthor(int id)
        {
            if (default(int) == id)
            {
                throw new ArgumentNullException("id");
            }

            var author = await this._authorRepository.GetById(id);
            await this._authorRepository.Delete(author);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await this._authorRepository.GetAllWithBooks();
        }

        public async Task<IEnumerable<Author>> GetAuthors(Expression<Func<Author, bool>> predicate)
        {
            return await this._authorRepository.GetData(predicate);
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException("author");

            var _dbAuthor = await this._authorRepository.GetById(author.Id);
            if (_dbAuthor == null)
            {
                throw new Exception("Not valid");
            }

            _dbAuthor.Name = author.Name;            
            _dbAuthor.PhotoName = author.PhotoName;

            return await this._authorRepository.Update(_dbAuthor);
        }
    }
}
