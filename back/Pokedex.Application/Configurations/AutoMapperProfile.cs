using AutoMapper;
using Pokedex.Application.Dtos.Users;
using Pokedex.Domain.Entities;

namespace Pokedex.Application.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
    }
}