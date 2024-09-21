using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Mapping;

public class AppMappingProfile: Profile
{
    public AppMappingProfile()
    {
        CreateMap<Category, RetriveCategoryDto>();
        CreateMap<CreateItemDto, Item>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<CreateUserDto, User>();
    }
}