using Ecom.core.Interfaces;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }
        public IProductPhotoRepository ProductPhoto { get; }

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Category= new CategoryRepository(_context);
            Product= new ProductRepository(_context);
            ProductPhoto= new ProductPhotoRepository(_context);
        }

        public int Commit()
        {
            var result= _context.SaveChanges();
            return result;
        }

        public Task<int> CommitAsync()
        {
            var result = _context.SaveChangesAsync();
            return result;
        }
    }
}
