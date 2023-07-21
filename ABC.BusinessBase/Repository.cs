using ABC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ABC.BusinessBase
{
    public class Repository<T> : IReposistory<T> where T : class, TEntity, new()
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entity;

        public Repository(DbContext context)
        {
            this._context = context;
            this._entity = context.Set<T>();
        }

        public async Task Add(T obj)
        {
           await this._entity.AddAsync(obj);                      
        }

        public async Task<bool> Delete(T obj)
        {
            var data = await GetById(obj.Id);

            if (data != null)
            {
                this._entity.Remove(obj);                
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<T>> GetAll()
        {           
            var  data = await this._entity.ToListAsync();                                  
            return data;
        }

        public async Task<T> GetById(int id)
        {
            var data = await this._entity.FirstOrDefaultAsync(x => x.Id == id);

            return data ?? new T();
        }

        public async Task<T> Update(T obj)
        {
            var data = await this._entity.FirstOrDefaultAsync(x => x.Id == obj.Id);

            if (data != null)
            {
                this._entity.Update(obj);                
                return obj;
            }

            throw new Exception("Not valid");
        }

        public async Task<int> Save()
        {
            return await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetData(Expression<Func<T, bool>> predicate)
        {
            var data = await this._entity.Where(predicate).ToListAsync();
            return data;
        }
    }
}
