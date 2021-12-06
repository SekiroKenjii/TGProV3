using AutoMapper;
using Core.DTOs.Authentication;
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
        }
    }
}
