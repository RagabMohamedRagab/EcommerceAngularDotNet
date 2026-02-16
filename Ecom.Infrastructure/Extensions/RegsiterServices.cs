using Ecom.core.Interfaces;
using Ecom.core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Extensions
{
    public static class RegsiterServices
    {
        public static void RegisterServiceConfiguration(this IServiceCollection services, IConfigurationManager configuration )
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IImageSaveService, ImageSaveService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory())));

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("EcomDb")));
        }
    }
}
