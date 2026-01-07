using ABC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abc.Infrastructure
{
    internal class AbcContext : DbContext
    {        
        public AbcContext(DbContextOptions<AbcContext> options): base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<Token> AuthToken {get; set;}
    }
}
