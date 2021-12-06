using AutoMapper;
using Core.DTOs.Authentication;
using Core.DTOs.Category;
using Core.DTOs.User;
using Domain.Entities;

namespace Core.AutoMappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();

            //Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryDto, Category>();
        }
    }
}
