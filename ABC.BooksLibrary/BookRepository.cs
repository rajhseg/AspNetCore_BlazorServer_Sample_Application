using ABC.BusinessBase;
using ABC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.BooksLibrary
{
    public class BookRepository : Repository<Book>, IBooksRepository 
    {
        public BookRepository(DbContext context): base(context)
        {

        }

        public async Task updateDescriptionAsync(int id, string description)
        {
            var data = await GetById(id);
            if(data != null)
            {
                data.Description = description;
                await Update(data);
            }

            throw new Exception("Not Valid");
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthors()
        {
            AbcContext? context = this._context as AbcContext;
            
            if(context==null)
                return Enumerable.Empty<Book>();

            return await context.Books.Include(x => x.Author).ToListAsync();
        }
    }
}
