using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<CreatedCategoryDto, Category>().ReverseMap();
        CreateMap<RetrivedCategoriesDto, Category>().ReverseMap();
        CreateMap<RetrivedCategoryDto, Category>()
            .ForMember(c => c.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<UpdatedCategoryDto, Category>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));
        
        CreateMap<CreatedRolesDto, Role>().ReverseMap();
        CreateMap<RetrivedRolesDto, Role>().ReverseMap();
        CreateMap<Role, RetrivedRoleDto>()
            .ForMember(r => r.Users, expression => expression.MapFrom(src => src.Users ))
            .ReverseMap();
        CreateMap<UpdatedRoleDto, Role>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));

        CreateMap<CreatedItemDto, Item>().ReverseMap();
        CreateMap<RetrivedItemDto, Item>().ReverseMap();
        CreateMap<UpdatedItemDto, Item>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));
        
        CreateMap<CreatedUserDto, User>().ReverseMap();
        CreateMap<RetrivedUsersDto, User>().ReverseMap();
        CreateMap<RetrivedUserDto, User>()
            .ForMember(u => u.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<UpdatedUserDto, User>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));
    }
}