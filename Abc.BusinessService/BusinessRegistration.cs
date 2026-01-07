using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abc.BusinessService
{
    public static class BusinessRegistration
    {
        public static void RegisterBusinessService(this IServiceCollection service) 
        {
            service.AddTransient<IAuthorService, AuthorService>();
            service.AddTransient<IBookService, BookService>();
            service.AddTransient<ITokenService, TokenService>();
        }
    }
}
