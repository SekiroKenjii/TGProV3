using AutoMapper;
using Core.DTOs.Authentication;
using Core.DTOs.Brand;
using Core.DTOs.Category;
using Core.DTOs.Condition;
using Core.DTOs.SubBrand;
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
            CreateMap<Category, CompactCategoryDto>().ReverseMap();
            CreateMap<AddCategoryDto, Category>();

            //Brand
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, CompactBrandDto>().ReverseMap();
            CreateMap<AddBrandDto, Brand>();

            //SubBrand
            CreateMap<SubBrand, SubBrandDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category))
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand));

            //Condition
            CreateMap<Condition, ConditionDto>().ReverseMap();
            CreateMap<Condition, CompactConditionDto>().ReverseMap();
            CreateMap<AddConditionDto, Condition>();
        }
    }
}
