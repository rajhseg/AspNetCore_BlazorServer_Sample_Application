using ABC.Entities;
using ABC.Entities.Interfaces;
using System.Linq.Expressions;

namespace Abc.BusinessService
{
    internal class AuthorService : IAuthorService
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
            _dbAuthor.PhotoContent = author.PhotoContent;
            return await this._authorRepository.Update(_dbAuthor);
        }
    }
}
