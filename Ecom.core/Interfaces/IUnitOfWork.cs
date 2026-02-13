using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }
        public IProductPhotoRepository ProductPhoto { get; }


        public int Commit();
        public Task<int> CommitAsync();
    }
}
