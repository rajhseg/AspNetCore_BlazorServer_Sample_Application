using ABC.Entities;
using ABC.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abc.BusinessService
{
    internal class BookService : IBookService
    {
        private readonly IBooksRepository _booksRepository;

        public BookService(IBooksRepository repository)
        {
          this._booksRepository = repository;
        }

        public async Task AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            await this._booksRepository.Add(book);
        }

        public async Task DeleteBook(int id)
        {
            if(default(int) == id)
            {
                throw new ArgumentNullException("id");
            }
            
            var book = await this._booksRepository.GetById(id);
            await this._booksRepository.Delete(book);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await this._booksRepository.GetAllWithAuthors();
        }

        public async Task<IEnumerable<Book>> GetBooks(Expression<Func<Book, bool>> predicate)
        {
            return await this._booksRepository.GetAllWithAuthors(predicate);
        }

        public async Task<Book> UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            var _dbBook = await this._booksRepository.GetById(book.Id);
            if (_dbBook == null)
            {
                throw new Exception("Not valid");
            }

            _dbBook.Description = book.Description;
            _dbBook.Title = book.Title;
            _dbBook.AuthorId = book.AuthorId;

            return await this._booksRepository.Update(_dbBook);
        }

    }
}
