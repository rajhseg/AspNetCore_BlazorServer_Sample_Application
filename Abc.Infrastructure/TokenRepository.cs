using ABC.Entities;
using ABC.Entities.Interfaces;
using ABC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Infrastructure
{
    internal class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(DbContext context) : base(context)
        {

        }
    }
}
