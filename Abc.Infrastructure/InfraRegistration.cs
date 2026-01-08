using ABC.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Infrastructure
{
    /* Composition Root */
    public static class InfraRegistration
    {
        public static void RegisterRepositories(this IServiceCollection service, string connectionString) 
        {
                
            service.AddDbContext<DbContext, AbcContext>(options => options.UseSqlServer(connectionString,
                                                                x => x.MigrationsAssembly("ABC.Infrastructure")),
                                                                contextLifetime: ServiceLifetime.Scoped);

            service.AddScoped<IAuthorRepository, AuthorRepository>();
            service.AddScoped<IBooksRepository, BookRepository>();
            service.AddScoped<ITokenRepository, TokenRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
