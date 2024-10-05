using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<CreatedCategoryDto, Category>().ReverseMap();
        CreateMap<RetrievedCategoriesDto, Category>().ReverseMap();
        CreateMap<RetrievedCategoryDto, Category>()
            .ForMember(c => c.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<UpdatedCategoryDto, Category>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));
        
        CreateMap<CreatedRoleDto, Role>().ReverseMap();
        CreateMap<RetrievedRolesDto, Role>().ReverseMap();
        CreateMap<Role, RetrievedRoleDto>()
            .ForMember(r => r.Users, expression => expression.MapFrom(src => src.Users ))
            .ReverseMap();
        CreateMap<UpdatedRoleDto, Role>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));

        CreateMap<CreatedItemDto, Item>().ReverseMap();
        CreateMap<RetrievedItemDto, Item>().ReverseMap();
        CreateMap<UpdatedItemDto, Item>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));
        
        CreateMap<CreatedUserDto, User>().ReverseMap();
        CreateMap<RetrievedUsersDto, User>().ReverseMap();
        CreateMap<RetrievedUserDto, User>()
            .ForMember(u => u.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<UpdatedUserDto, User>()
            .ForAllMembers(ops => ops
                .Condition((_, _, srcMember) => srcMember != null));
    }
}