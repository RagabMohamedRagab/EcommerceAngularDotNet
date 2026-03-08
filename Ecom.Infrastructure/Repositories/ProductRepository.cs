using AutoMapper;
using Ecom.core.Dtos;
using Ecom.core.Entities.Models;
using Ecom.core.Exceptions;
using Ecom.core.Interfaces;
using Ecom.core.Services;
using Ecom.core.Shared;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IImageSaveService _imageSaveService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageSaveService imageSaveService) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _imageSaveService = imageSaveService;
        }
        public async Task<List<ProductDto>> GetAllProducts(ProductParameter parameter)
        {
            var query = _context.Products
                .Include(b => b.Category)
                .Include(b => b.ProductPhotos)
                .AsNoTracking();

            if (string.IsNullOrEmpty(parameter.Search))
            {
                var words = parameter.Search.Split(' ');
                query = query.Where(b => words.All(
                    w => b.Name.ToLower().Contains(w.ToLower())
                    ||
                    b.Description.ToLower().Contains(w.ToLower())
                    ));
            }
            if (parameter.CategoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == parameter.CategoryId);
            }

            if (!string.IsNullOrEmpty(parameter.sort))
            {
                switch (parameter.sort)
                {
                    case "PriceAsn":
                        query = query.OrderBy(b => b.NewPrice);
                        break;
                    case "PriceDsc":
                        query = query.OrderByDescending(b => b.NewPrice);
                        break;
                    default:
                        query = query.OrderBy(b => b.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(b => b.Name);
            }

            var pageSizeLocal = parameter.pageSize > 0 ? parameter.pageSize : 3;
            var pageNumberLocal = parameter.PageNumber > 0 ? parameter.PageNumber : 1;

            var TotalItems = await query.CountAsync();

            var products = await query
                .Skip((pageNumberLocal - 1) * pageSizeLocal)
                .Take(pageSizeLocal)
                .ToListAsync();

            var mapped = _mapper.Map<List<ProductDto>>(products);
            return mapped;
        }
        public async Task<bool> AddProduct(AddProudct proudct)
        {
            if (proudct == null) throw new BusineesException("Product Not Found");

            var result = _mapper.Map<Product>(proudct);

            await AddAsync(result);
            await _context.SaveChangesAsync();

            List<string> images = await _imageSaveService.SaveImgae(proudct.file, proudct.Name);

            var Photos = images.Select(b => new ProductPhoto()
            {
                ImageName = b,
                ProductId = result.Id
            });

            _context.ProductPhotos.AddRange(Photos);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateProduct(ProductUpdate productUpdate)
        {
            try { 
            var Product =await GetById(productUpdate.Id, b => b.Category, b => b.ProductPhotos);
            if (Product is null)
            {
                return false;
            }
            _mapper.Map<Product>(productUpdate);

                if (productUpdate.file != null && productUpdate.file.Count > 0)
                {
                    foreach (var file in Product.ProductPhotos)
                    {
                        await _imageSaveService.DeleteImage(file.ImageName);
                        File.Delete(file.ImageName);
                    }
                    _context.ProductPhotos.RemoveRange(Product.ProductPhotos);
                   var photo= await _imageSaveService.SaveImgae(productUpdate.file, productUpdate.Name);
                    var Photos = photo.Select(b => new ProductPhoto()
                    {
                        ImageName = b,
                        ProductId = Product.Id
                    }).ToList();
                    _context.ProductPhotos.AddRange(Photos);
                }
               await _context.SaveChangesAsync();
               return true;
            }
            catch 
            {
                return false;
            }
        }

    }
}
