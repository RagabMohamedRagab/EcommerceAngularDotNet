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
        }
    }
}
