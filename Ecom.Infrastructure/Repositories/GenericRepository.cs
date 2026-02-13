using Ecom.core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> table;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await table.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            table.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return table.AsNoTracking().ToList();
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] expression)
        {
            var query= table.AsNoTracking().AsQueryable();
            foreach(var predicate in expression)
            {
                query = query.Include(predicate);
            }
            return query.ToList();
        }

        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<T> GetById(int id, params Expression<Func<T, object>>[] expression)
        {
            var query = table.AsNoTracking().AsQueryable();
            foreach (var predicate in expression)
            {
                query = query.Include(predicate);
            }
            return await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
        }

        public async Task UpdateAsync(T entity)
        {
            table.Update(entity);
        }
    }
}
