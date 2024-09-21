using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Mapping;

public class AppMappingProfile: Profile
{
    public AppMappingProfile()
    {
        CreateMap<CreateCategoryDto, Category>().ReverseMap();
        CreateMap<CreateItemDto, Item>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<CreateUserDto, User>();
    }
}