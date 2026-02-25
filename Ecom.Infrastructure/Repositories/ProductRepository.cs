using AutoMapper;
using Ecom.core.Dtos;
using Ecom.core.Entities.Models;
using Ecom.core.Exceptions;
using Ecom.core.Interfaces;
using Ecom.core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
