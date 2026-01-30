
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
    internal class TokenService : ITokenService
    {
        private readonly ITokenRepository tokenRepository;

        public TokenService(ITokenRepository Repository)
        {
            this.tokenRepository = Repository;            
        }

        public async Task AddToken(Token token)
        {
            if(token==null)
                throw new ArgumentException("token");

            await this.tokenRepository.Add(token);
        }

        public async Task DeleteToken(int id)
        {
            var token = await this.tokenRepository.GetById(id);

            if(token!=null){
                await this.tokenRepository.Delete(token);
            }
        }

        public async Task<Token> GetActualToken(string encryptedToken)
        {
           var result =  await this.tokenRepository.GetData(x=>x.ClientToken.ToLower()==encryptedToken.ToLower());
           var actualToken = result.FirstOrDefault();

           if(actualToken==null)
            throw new Exception("Token invalid");

            return actualToken;
        }

        public async Task<Token> UpdateToken(string refreshToken, string encryptedToken, string actualToken)
        {
            var result = (await this.tokenRepository.GetData(x=>x.RefreshToken.ToLower()==refreshToken.ToString())).FirstOrDefault();

            if(result==null)
            throw new Exception("invalid token");

            result.ActualToken = actualToken;
            result.ClientToken = encryptedToken;

            await this.tokenRepository.Update(result);
            
            return result;
        }

        public async Task<Token> GetData(string token)
        {
            var tokendata = (await this.tokenRepository.GetData(x=>x.ActualToken.ToLower()==token.ToLower())).FirstOrDefault();
            return tokendata;
        }
    }
}