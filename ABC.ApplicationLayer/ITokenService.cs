using ABC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abc.BusinessService
{
    public interface ITokenService{

        Task<Token> UpdateToken(string refreshToken, string encrptedToken, string actualToken);

        Task<Token> GetActualToken(string encryptedToken);

        Task<Token> GetData(string token);

        Task DeleteToken(int id);

        Task AddToken(Token token);
    }
}