using ABC.Entities;
using ABC.Entities.Interfaces;
using ABC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace Abc.Infrastructure
{
    internal class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DbContext context) : base(context)
        {

        }

        public async Task UpdateAuthorName(int id, string name)
        {
            var data = await GetById(id);
            if (data != null)
            {
                data.Name = name;
                await Update(data);
            }

            throw new Exception("Not valid");
        }

        public async Task<IEnumerable<Author>> GetAllWithBooks()
        {
            AbcContext? context = this._context as AbcContext;

            if (context == null)
                return Enumerable.Empty<Author>();

            return await context.Authors.Include(x => x.Books).ToListAsync();
        }
    }

}
