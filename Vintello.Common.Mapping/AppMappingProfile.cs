using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Category, RetrivedCategoriesDto>().ReverseMap();
        CreateMap<Category, CreatedUpdatedCategoryDto>().ReverseMap();
        CreateMap<Category, RetrivedCategoryDto>()
            .ForMember(c => c.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        
        CreateMap<CreatedUpdatedRetrivedRolesDto, Role>().ReverseMap();
        CreateMap<Role, RetrivedRoleDto>()
            .ForMember(r => r.Users, expression => expression.MapFrom(src => src.Users ))
            .ReverseMap();

        CreateMap<CreatedItemDto, Item>().ReverseMap();
        CreateMap<RetrivedItemDto, Item>().ReverseMap();
        CreateMap<UpdatedItemDto, Item>().ReverseMap();

        CreateMap<UpdatedUserDto, User>().ReverseMap();
        CreateMap<RetrivedUsersDto, User>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<RetrivedUserDto, User>()
            .ForMember(u => u.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
    }
}