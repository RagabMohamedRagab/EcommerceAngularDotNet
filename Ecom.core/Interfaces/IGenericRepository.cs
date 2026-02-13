using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] expression);

        public Task<T> GetById(int id);

        public Task<T> GetById(int id,params Expression<Func<T,object>> [] expression);

        public Task AddAsync(T entity);
       public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
