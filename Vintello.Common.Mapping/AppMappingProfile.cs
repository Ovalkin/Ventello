using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Category, RetrivedCategoriesDto>().ReverseMap();
        CreateMap<Category, CreatedCategoryDto>().ReverseMap();
        CreateMap<Category, RetrivedCategoryDto>()
            .ForMember(c => c.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<UpdatedCategoryDto, Category>()
            .ForAllMembers(ops => ops
                .Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<CreatedRolesDto, Role>().ReverseMap();
        CreateMap<RetrivedRolesDto, Role>().ReverseMap();
        CreateMap<UpdatedRoleDto, Role>()
            .ForAllMembers(ops => ops
                .Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Role, RetrivedRoleDto>()
            .ForMember(r => r.Users, expression => expression.MapFrom(src => src.Users ))
            .ReverseMap();

        CreateMap<CreatedItemDto, Item>().ReverseMap();
        CreateMap<RetrivedItemDto, Item>().ReverseMap();
        CreateMap<UpdatedItemDto, Item>()
            .ForAllMembers(ops => ops
                .Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<RetrivedUsersDto, User>().ReverseMap();
        CreateMap<CreatedUserDto, User>().ReverseMap();
        CreateMap<RetrivedUserDto, User>()
            .ForMember(u => u.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<UpdatedUserDto, User>()
            .ForAllMembers(ops => ops
                .Condition((src, dest, srcMember) => srcMember != null));
    }
}