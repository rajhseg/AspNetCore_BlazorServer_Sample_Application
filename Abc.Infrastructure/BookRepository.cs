using ABC.Entities;
using ABC.Entities.Interfaces;
using ABC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Abc.Infrastructure
{
    internal class BookRepository : Repository<Book>, IBooksRepository
    {
        public BookRepository(DbContext context) : base(context)
        {

        }

        public async Task updateDescriptionAsync(int id, string description)
        {
            var data = await GetById(id);
            if (data != null)
            {
                data.Description = description;
                await Update(data);
            }

            throw new Exception("Not Valid");
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthors()
        {
            AbcContext? context = this._context as AbcContext;

            if (context == null)
                return Enumerable.Empty<Book>();

            return await context.Books.Include(x => x.Author).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthors(Expression<Func<Book, bool>> predicate)
        {
            AbcContext? context = this._context as AbcContext;

            if (context == null)
                return Enumerable.Empty<Book>();

            return await context.Books.Include(x => x.Author).Where(predicate).ToListAsync();
        }
    }
}
