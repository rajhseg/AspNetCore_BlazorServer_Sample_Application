using ABC.BusinessBase;
using ABC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abc.AuthorLibrary
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DbContext context): base(context)
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
