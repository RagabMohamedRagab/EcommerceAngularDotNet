using Ecom.core.Entities.Models;
using Ecom.core.Interfaces;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductPhotoRepository : GenericRepository<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
