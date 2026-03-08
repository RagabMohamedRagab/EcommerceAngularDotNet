using Ecom.core.Dtos;
using Ecom.core.Entities.Models;
using Ecom.core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<List<ProductDto>> GetAllProducts(ProductParameter parameter);
        Task<bool> AddProduct(AddProudct proudct);

        Task<bool> UpdateProduct(ProductUpdate productUpdate);

       
    }
}
