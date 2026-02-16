using Ecom.core.Dtos;
using Ecom.core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {

        Task<bool> AddProduct(AddProudct proudct);
    }
}
