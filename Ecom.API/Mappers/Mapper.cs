using AutoMapper;
using Ecom.core.Dtos;
using Ecom.core.Entities.Models;

namespace Ecom.API.Mappers
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategory>().ReverseMap();
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<ProductPhoto, PhottoDto>().ReverseMap();
            CreateMap<AddProudct, Product>().ForMember(b=>b.ProductPhotos,op=>op.Ignore()).ReverseMap();
            CreateMap<ProductUpdate, Product>().ForMember(b=>b.ProductPhotos,op=>op.Ignore()).ReverseMap();
        }
    }
}
