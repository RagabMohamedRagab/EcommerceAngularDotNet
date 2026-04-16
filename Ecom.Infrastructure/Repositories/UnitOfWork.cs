using AutoMapper;
using Ecom.core.Interfaces;
using Ecom.core.Services;
using Ecom.Infrastructure.Data;
using StackExchange.Redis;
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

        public ICustomerBasketItemRepository CustomerBasketItem { get; }

        private readonly AppDbContext _context;
        private readonly IImageSaveService _imageSaveService;
        private readonly IMapper _mapper;
        private readonly IDatabase _database;
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageSaveService imageSaveService, IConnectionMultiplexer redis) 
        {
            _context = context;
            _mapper = mapper;
            _imageSaveService = imageSaveService;
            _database= redis.GetDatabase();
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context, _mapper, _imageSaveService);
            ProductPhoto = new ProductPhotoRepository(_context);
            CustomerBasketItem = new CustomerBasketItemRepository(_database);
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
